import { Component, OnInit, Input, ViewEncapsulation} from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { OrderService } from 'src/app/_services/order.service';
import { CartService } from 'src/app/_services/cart.service';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ShopService } from 'src/app/_services/shop.service';
import { VoucherService } from 'src/app/_services/voucher.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],


})
export class OrderComponent implements OnInit {

  @Input() vouchers:any
  @Input() orderVouchers:any
  order : any;
  dataString : any;
  sumPrice : number = 0;
  address :string = "54 Nguyễn Lương Bằng, Hoà Khánh Bắc, Liên Chiểu, Đà Nẵng 550000, Việt Nam";
  orderSend :any;
  target : any;
  orderVoucherValue :number =0;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private orderService : OrderService,
    private modalService: NgbModal,
    private shopService :ShopService,
    private voucherService: VoucherService,
    private cartService: CartService,
  ) { }

  ngOnInit(): void {
    this.loadOrder()
    this.loadSumPrice()
  }

  loadOrder(){
    this.activatedRoute.queryParams.subscribe(params => {
      let data = params;
      this.dataString = data;
      console.log(data);
      this.order = JSON.parse(this.dataString.data);
      console.log(this.order);
    });
    this.loadVoucherShop()
    this.order.map((obj:any) => {
      obj.voucherProductId =0,
      obj.totalPrice= obj.price*obj.amount,
      obj.voucherProductId =0;
      return obj;
    })
  }

  createOrder(){
    this.orderSend =this.order.map((obj: any) => ({
      amount : obj.amount,
      price : obj.totalPrice,
      productDetailId : obj.idProductDetail,
      voucherProductId : obj.voucherProductId
    }))
    console.log(this.orderSend);
    // this.orderSend.map((obj:any) => {
    //   obj.orderId=0,
    //   obj.voucherProductId =0,
    //   obj.note = "nothing";
    //   return obj;
    // })

    const submitData = {
      "id": 0,
      "state": 0,
      "address": this.address,
      "recipientName": "Thinhnguyen",
      "recipientPhone": "0965243513",
      "userId": 0,
      "voucherId": 1,
      "paymentMethodId": 0,
      "totalPrice": 3000,
      "itemOrderDtos": this.orderSend

    }
    this.orderService.addOrder(submitData)
    .subscribe(
      (res:any) => {
        //return home
        this.router.navigate(['/'])
        alert("them dc roi")
      },
      (err) => {
        alert("k them dc")
      })
      this.deleteItem()
  }

  openModalShopVoucher(content: any,target :any) {
    this.target=target
    this.order[this.target].voucherProductId = 0
		this.modalService.open(content, { centered: true });
    this.order[this.target].totalPrice = this.order[this.target].price *this.order[this.target].amount
	}

  openModalOrderVoucher(content: any) {
    this.orderVoucherValue = 0
		this.modalService.open(content, { centered: true });
    this.loadVoucherOrder()
	}

  loadVoucherShop(){
    this.shopService.getAllVoucherShop()
    .subscribe(
      (res) => this.handleGetVoucherSuccess(res),
      (err) => this.handleGetVoucherError(err)
    )
  }

  handleGetVoucherError(err: any){
    console.log(err)
  }
  handleGetVoucherSuccess(res: any){
    this.vouchers = res.result.data
    console.log(res)
  }

  loadVoucherOrder(){
    this.voucherService.getVoucherAvaiable()
    .subscribe(
      (res) => this.handleGetOrderVoucherSuccess(res),
      (err) => this.handleGetOrderVoucherError(err)
    )
  }
  handleGetOrderVoucherError(err: any){
    console.log(err)
  }
  handleGetOrderVoucherSuccess(res: any){
    this.orderVouchers = res.result.data
    console.log(this.orderVouchers);

  }

  selectedShoprVoucher(val:any,id:any){
    this.order[this.target].totalPrice = this.order[this.target].price *this.order[this.target].amount - val
    this.order[this.target].voucherProductId = id;
    this.loadSumPrice()
  }
  selectedOrderVoucher(val:any){
    this.orderVoucherValue = val
    this.loadSumPrice()
  }

  loadSumPrice(){
    this.sumPrice =0
    for (let i =0 ; i < this.order.length ; i++){
      this.sumPrice += this.order[i].totalPrice  ;
      console.log(this.sumPrice);

    }
    this.sumPrice -= this.orderVoucherValue
  }
  deleteItem(){
    if(window.confirm("Ban thuc su muon xoa")){
      for (let i=0 ; i< this.order.length; i++){
        this.cartService.deleteItem(this.order[i].id)
      .subscribe(
        (res:any) => {

        },
        (err) => {
          alert("Delete fail. Detail: " + JSON.stringify(err))
        }
      )
      }

    }

  }
}
