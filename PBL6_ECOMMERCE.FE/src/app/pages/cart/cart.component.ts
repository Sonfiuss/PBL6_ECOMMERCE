import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  cart = [
    {
      'id':1,
      'name':'thinhgnuyen',
      'price':399000
    },
    {
      'id':2,
      'name':'thinhgnuyen',
      'price':399000
    },
    {
      'id':3,
      'name':'thinhgnuyen',
      'price':399000
    },
    {
      'id':4,
      'name':'thinhgnuyen',
      'price':399000
    },
  ];
  change(){
    this.cart.splice(1,1);
  }
}
