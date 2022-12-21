import { Component, OnInit, Input} from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { OrderService } from 'src/app/_services/order.service';
import { CartService } from 'src/app/_services/cart.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {


  order : any;
  dataString : any;
  sumPrice : number = 0;
  address :string = "54 Nguyễn Lương Bằng, Hoà Khánh Bắc, Liên Chiểu, Đà Nẵng 550000, Việt Nam";
  order2 :any;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private orderService : OrderService
  ) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      let data = params;
      this.dataString = data;
      console.log(data);
      this.order = JSON.parse(this.dataString.data);
      // console.log(this.order);
      console.log(this.order);
    });


    // console.log(this.order[0]);
    // this.dataString = JSON.parse(this.activatedRoute.snapshot.paramMap.get('my_order'));
  }

  createOrder(){
    this.order2 =this.order.map((obj: any) => ({
      amount : obj.amount,
      price : obj.price,
      productDetailId : obj.idProductDetail

    }))
    console.log(this.order2);
    this.order2.map((obj:any) => {
      obj.orderId=0,
      obj.voucherProductId =1,
      obj.note = "nothing";
      return obj;
    })
    console.log(this.order);
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
      "itemOrderDtos": this.order2

    }
    console.log(submitData)
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
  }

}
