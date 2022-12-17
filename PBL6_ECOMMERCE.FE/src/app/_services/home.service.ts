import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusinessService } from './business.service';

const API =  'https://localhost:7220';
const GET_ALL_PRODUCT = API +  '/api/Home/get-list-product';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(
    private httpClient: HttpClient,
    private bussiness: BusinessService
  ) { }

  getAllProduct(){
    console.log("daica123");
    return this.httpClient.get(GET_ALL_PRODUCT, this.bussiness.getRequestOptions())
  }
}
