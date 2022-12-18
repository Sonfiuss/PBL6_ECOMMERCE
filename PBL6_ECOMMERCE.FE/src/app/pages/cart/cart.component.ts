import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IsActiveMatchOptions } from '@angular/router';
import { CartService } from 'src/app/_services/cart.service';
import { VoucherService } from 'src/app/_services/voucher.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  @Input() cart:any
  @Output() deletecartEvent = new EventEmitter<number>()
  @Input() vouchers:any
  data = {id : "1", username : "thinh"}
  constructor(
    private cartService : CartService,
    private voucherService : VoucherService,
    private router: Router,
    // private transfereService :TransfereService
  ) { }

  // cart :Array<any>;
  order : Array<any> =[];
  sumPrice : number = 0;
  shopVoucher = {
    id : 1,
    value  : 100000
  }

  isChecked :Array<any> ;
  ngOnInit(): void {
    this.loadCart();
    this.checkarr();
    this.showVoucher();
  }

  change(){
    this.cart.splice(1,1);
  }

  loadCart(){
    this.cartService.getCart(2)
    .subscribe(
      (res) => this.handleGetCartSuccess(res),
      (err) => this.handleGetCartError(err)
    )
  }

  handleGetCartError(err: any){
    console.log(err);
    console.log("thinhnguyen1233456");
  }
  handleGetCartSuccess(res: any){
    this.cart = res.result.data
    console.log(res)
  }
  checkarr(){
    this.isChecked = new Array(100);
    for( let i =0; i < this.isChecked.length;i++){
      this.isChecked[i] = false;
    }

  }
  checkValue(event : any,pd :any){
    if (event ==  true){
      this.order.push(pd)
      // console.log(event)
      console.log(this.order)
      // console.log(this.order.length )
      this.sumPrice += pd.price * pd.amount;
      // for (let j = 0; j< this.order.length; j++){
      //   this.sumPrice += this.order[j].price *this.order[j].amount;
      // }
    }
    else{
      this.order = this.order.filter(item => item !== pd)
      // console.log(event)
      // console.log(this.order.length )
      // console.log(this.cart.length)
      // console.log(this.isChecked)
      this.sumPrice -= pd.price * pd.amount;
      // for (let j = 0; j< this.order.length; j++){
      //   this.sumPrice = this.order[j].price *this.order[j].amount;
      // }
    }
  }
  deleteItem(pd : any){
    if(window.confirm("Ban thuc su muon xoa")){
      this.cartService.deleteItem(pd.id)
      .subscribe(
        (res:any) => {
          this.deletecartEvent.emit(pd.id)
          this.cart = this.cart.filter((item:any) => item !==pd)
          console.log(this.cart);
        },
        (err) => {
          alert("Delete fail. Detail: " + JSON.stringify(err))
        }
      )
    }
    this.order = this.order.filter(item => item !== pd)
    // this.cartService.deleteItem(pd.id)
  }

  decreaseQty(pd : any, i:any){

    if (pd.amount > 1){
      pd.amount -= 1;
      if(this.isChecked[i] ){
        this.order = this.order.filter(item => item !== pd)
        this.sumPrice -= pd.price
        this.order.push(pd)
      }
    }
  }

  increaseQty(pd : any, i: any){
    pd.amount += 1;
    if(this.isChecked[i] ){
      this.order = this.order.filter(item => item !== pd)
      this.sumPrice += pd.price
      this.order.push(pd)
    }
  }

  showVoucher(){
    this.voucherService.getVoucherAvaiable()
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
  check(order: any){
    console.log(order);
    let data = JSON.stringify(order);
    this.router.navigate(['/order'], { queryParams:  {data} ,  skipLocationChange: true });
    // this.router.createUrlTree(['/order', {my_order: JSON.stringify(order)}]);

  }

}



