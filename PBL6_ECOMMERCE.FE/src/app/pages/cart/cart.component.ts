import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IsActiveMatchOptions } from '@angular/router';
import { CartService } from 'src/app/_services/cart.service';
@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  @Input() cart:any
  @Output() deletecartEvent = new EventEmitter<number>()
  constructor(
    private cartService : CartService
  ) { }

  // cart :Array<any>;
  oder : Array<any> =[];
  sumPrice : number = 0;
  shopVoucher = {
    id : 1,
    value  : 100000
  }

  isChecked :Array<any> ;
  ngOnInit(): void {
    this.loadCart();
    this.checkarr();
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
  checkarr(){
    this.isChecked = new Array(100);
    for( let i =0; i < this.isChecked.length;i++){
      this.isChecked[i] = false;
    }

  }
  handleGetCartError(err: any){
    console.log(err)
    console.log("get done")
  }
  handleGetCartSuccess(res: any){
    this.cart = res.result.data
    console.log(res)
    console.log("get done")
  }

  checkValue(event : any,pd :any){
    if (event ==  true){
      this.oder.push(pd)
      // console.log(event)
      console.log(this.oder)
      // console.log(this.oder.length )
      this.sumPrice += pd.price * pd.amount;
      // for (let j = 0; j< this.oder.length; j++){
      //   this.sumPrice += this.oder[j].price *this.oder[j].amount;
      // }
    }
    else{
      this.oder = this.oder.filter(item => item !== pd)
      // console.log(event)
      // console.log(this.oder.length )
      // console.log(this.cart.length)
      // console.log(this.isChecked)
      this.sumPrice -= pd.price * pd.amount;
      // for (let j = 0; j< this.oder.length; j++){
      //   this.sumPrice = this.oder[j].price *this.oder[j].amount;
      // }
    }
  }
  deleteItem(pd : any){
    if(window.confirm("Ban thuc su muon xoa")){
      this.cartService.deleteItem(pd.id)
      .subscribe(
        (res:any) => {
          this.deletecartEvent.emit(pd.id)
        },
        (err) => {
          alert("Delete fail. Detail: " + JSON.stringify(err))
        }
      )
    }
    this.oder = this.oder.filter(item => item !== pd)
    // this.cartService.deleteItem(pd.id)
  }

  decreaseQty(pd : any, i:any){

    if (pd.amount > 1){
      pd.amount -= 1;
      if(this.isChecked[i] ){
        this.oder = this.oder.filter(item => item !== pd)
        this.sumPrice -= pd.price
        this.oder.push(pd)
      }
    }
  }

  increaseQty(pd : any, i: any){
    pd.amount += 1;
    if(this.isChecked[i] ){
      this.oder = this.oder.filter(item => item !== pd)
      this.sumPrice += pd.price
      this.oder.push(pd)
    }
  }


}


