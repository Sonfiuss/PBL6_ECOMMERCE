import { Injectable } from '@angular/core';
import { BusinessService } from './business.service';
import { HttpClient } from '@angular/common/http';

const API =  'https://localhost:7220'
const GET_ALL_ITEM_BY_USER_ID = (id:any) => API + '/api/Cart/get-all-items-of-user/' + id;
const ADD_ITEM_TO_CART = API + '/api/Cart/Add-item-to-cart' ;
const UPDATE_ITEM_IN_CART = API + 'api/Cart/update-item-in-cart' ;
const DELETE_ITEM_IN_CART = (id:any) => API + '/api/Cart/delete-item-in-cart/' + id;


@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(
    private httpClient: HttpClient,
    private businessService: BusinessService
  ) { }
  addItem2Cart(){
    return this.httpClient.post(ADD_ITEM_TO_CART , this.businessService.getRequestOptions())
  }
  updateItem(){
    return this.httpClient.post(UPDATE_ITEM_IN_CART, this.businessService.getRequestOptions())
  }
  deleteItem(id:any){
    return this.httpClient.delete(DELETE_ITEM_IN_CART(id), this.businessService.getRequestOptions())
  }
  getCart(id:any){
    console.log(id)
    return this.httpClient.get(GET_ALL_ITEM_BY_USER_ID(id), this.businessService.getRequestOptions())
  }

}
