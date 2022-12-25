using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Response;
using Website_Ecommerce.API.Repositories;
using Microsoft.EntityFrameworkCore;
using PBL6_ECOMMERCE.Website_Ecommerce.API.Response;
using PBL6_ECOMMERCE.Website_Ecommerce.API.ModelDtos;

namespace PBL6_ECOMMERCE.Website_Ecommerce.API.services
{
    public class Service : IServices
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        public Service(IConfiguration configurationManager,
            IOrderRepository orderRepository
        )
        {
            _configuration = configurationManager;
            _orderRepository = orderRepository;
        }
        public async Task<Response<ResponseDefault>> getPaymentLink(int orderId,string vnp_Returnurl)
        {
            Response<ResponseDefault> response = new Response<ResponseDefault>();
            try {
                string vnp_Url = _configuration["vnp_Url"]; //URL thanh toan cua VNPAY 
                string vnp_TmnCode = _configuration["vnp_TmnCode"]; //Ma website
                string vnp_HashSecret = _configuration["vnp_HashSecret"]; //Chuoi bi mat
                Order order = await _orderRepository.Orders.FirstOrDefaultAsync(x=> x.Id == orderId);
                if(order == null)
                {
                    response.State = false;
                    response.Message = "NotFound";  
                    return response;
                }
                VnPayLibrary vnpay = new VnPayLibrary();
                //Save order to db
                order.Reference = VnPayLibrary.GenerateReferences(10); // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
                order.State = 0; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" 1: thanh cong 2   that bai
                _orderRepository.Update(order);
                await _orderRepository.UnitOfWork.SaveAsync(new CancellationToken());

                //Build URL for VNPAY

                vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                vnpay.AddRequestData("vnp_Amount", (order.TotalPrice * 10000).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
                vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_Locale", "vn");
                vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.Reference);
                vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
                vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                vnpay.AddRequestData("vnp_TxnRef", order.Reference); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
                
                //Add Params of 2.1.0 Version
                vnpay.AddRequestData("vnp_ExpireDate",DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
                response.State = true;
                response.Message = "Success";
                response.Result = new ResponseDefault()
                {
                    Data = paymentUrl
                };
                return response;
            }
            catch (Exception e)
            {
                response.State = false;
                response.Message = e.Message;
                return response;
            }
        }

        public Task<ResponseVnPay> returnUrl(ReturnRequest request)
        {
            string returnContent = string.Empty;
            if (request == null)
            {
                string vnp_HashSecret = _configuration["vnp_HashSecret"]; //Secret key
                var vnpayData = request;
                VnPayLibrary vnpay = new VnPayLibrary();
                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //Lay danh sach tham so tra ve tu VNPAY
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount"))/100;
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    //Cap nhat ket qua GD
                    //Yeu cau: Truy van vao CSDL cua  Merchant => lay ra duoc OrderInfo
                    //Giả sử OrderInfo lấy ra được như giả lập bên dưới
                    OrderInfo order = new OrderInfo();//get from DB
                    order.OrderId = orderId;
                    order.Amount = 100000;
                    order.PaymentTranId = vnpayTranId;
                    order.Status = "0"; //0: Cho thanh toan,1: da thanh toan,2: GD loi
                    //Kiem tra tinh trang Order
                    if (order != null)
                    {
                        if (order.Amount == vnp_Amount) {
                            if (order.Status == "0")
                            {
                                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                                {
                                    //Thanh toan thanh cong
                                    log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId,
                                        vnpayTranId);
                                    order.Status = "1";
                                }
                                else
                                {
                                    //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                                    //  displayMsg.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                                    log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}",
                                        orderId,
                                        vnpayTranId, vnp_ResponseCode);
                                    order.Status = "2";
                                }

                                //Thêm code Thực hiện cập nhật vào Database 
                                //Update Database

                                returnContent = "{\"RspCode\":\"00\",\"Message\":\"Confirm Success\"}";
                            }
                            else
                            {
                                returnContent = "{\"RspCode\":\"02\",\"Message\":\"Order already confirmed\"}";
                            }
                        }
                        else
                        {
                            returnContent = "{\"RspCode\":\"04\",\"Message\":\"invalid amount\"}";
                        }
                    }
                    else
                    {
                        returnContent = "{\"RspCode\":\"01\",\"Message\":\"Order not found\"}";
                    }
                }
                else
                {
                    log.InfoFormat("Invalid signature, InputData={0}", Request.RawUrl);
                    returnContent = "{\"RspCode\":\"97\",\"Message\":\"Invalid signature\"}";
                }
            }
            else
            {
                returnContent = "{\"RspCode\":\"99\",\"Message\":\"Input data required\"}";
            }
        }
    }
}