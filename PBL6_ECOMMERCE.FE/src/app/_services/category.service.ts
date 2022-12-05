import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListCategory } from '../_models/category';
import { BusinessService } from './business.service';

const API =  'https://localhost:7220'
const LIST_CATEGORY = API + '/api/Category/list-category';
const ADD_CATEGORY_NAME_URL = API + '/api/Category/add';
const GET_CATEGORY_BY_ID = (id:any) => API + '/api/Category/get-by-id/' + id;
const UPDATE_CATEGORY_URL = API + '/api/Category/edit'
const DELETE_CATEGORY_BY_ID_URL = (id:any) => API + '/api/Category/delete/' + id;

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(
    private httpClient: HttpClient,
    private businessService: BusinessService
    ) { }

  // getCategory(): Observable<any>{
  //   return  this.httpClient.get<ListCategory>(this.baseUrl);
  // }
  getCategories(){
    return this.httpClient.get(LIST_CATEGORY, this.businessService.getRequestOptions())
  }
}
