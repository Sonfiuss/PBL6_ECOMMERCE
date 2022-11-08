using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Response;

namespace Website_Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherOrderRepository _voucherOrderRepository;
        
        private readonly IMapper _mapper;
        public VoucherController(IVoucherOrderRepository voucherOrder, IMapper mapper){
            _mapper = mapper;
            _voucherOrderRepository = voucherOrder;
        }

        [HttpPost("Add-voucher-by-Admin")]
        public async Task<IActionResult> Add ([FromBody] VoucherOrderDto request, CancellationToken cancellationToken)
        {   
            var item = _mapper.Map<VoucherOrder>(request);
            _voucherOrderRepository.Add(item);
            var result = await _voucherOrderRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result == 0)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExcuteDB,
                    Result = new ResponseDefault()
                    {
                        Data = "Add ProductImage fail"
                    }
                });
            }
            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Add ProductImage success"
                }
            });
        }

        [HttpPost("Update-voucher-by-Admin")]
        public async Task<IActionResult>Update([FromBody] VoucherOrderDto request, CancellationToken cancellationToken)
        {   
            var item = _mapper.Map<VoucherOrder>(request);
            _voucherOrderRepository.Update(item);
            var result = await _voucherOrderRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result == 0)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExcuteDB,
                    Result = new ResponseDefault()
                    {
                        Data = "Update Voucher Order fail"
                    }
                });
            }
            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Update Voucher Order success"
                }
            });
        }

        [HttpDelete("Delete-Voucher-by-Admin/{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            VoucherOrder voucherOrder = await _voucherOrderRepository.VoucherOrders.FirstOrDefaultAsync(p => p.Id == id);
            
            if(voucherOrder == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Not Found Vouchers"
                    }
                });
            }
            voucherOrder.Amount = 0;
            _voucherOrderRepository.Update(voucherOrder);
            var result = await _voucherOrderRepository.UnitOfWork.SaveAsync(cancellationToken);

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = voucherOrder.Id.ToString()
                    }
                });
            }
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Update Voucher fail"
                }
            });
        }
        [HttpGet("GetVoucherOrderby/{id}")]
        public async Task<IActionResult> GetVoucherOrderById(int id){
            var voucherOrder =  await _voucherOrderRepository.VoucherOrders.FirstOrDefaultAsync(p => p.Id ==id);
            if(voucherOrder is null){
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Not found Voucher"
                    }
                });
            }
            return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = voucherOrder
                    }
            });
        }

        [HttpGet("GetAllVoucherOrderbyAdmin")]
        public async Task<IActionResult> GetAllVoucherOrder(){
            var vouchersUnexpired =  await _voucherOrderRepository.VoucherOrders.Where(x => x.Expired>DateTime.Now).ToListAsync();
            if(vouchersUnexpired.Count() ==  0){
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Not found Voucher"
                    }
                });
            }
            return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = vouchersUnexpired
                    }
            });
        }
        [HttpGet("GetAllVoucherMatchWithOrder")]
        public async Task<IActionResult> GetAllVoucherOrderMatch(){
            var vouchersUnexpired =  await _voucherOrderRepository.VoucherOrders.Where(x => x.MinPrice> 10000).ToListAsync();
            if(vouchersUnexpired.Count() ==  0){
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Not found Voucher"
                    }
                });
            }
            return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = vouchersUnexpired
                    }
            });
        }
    }
}