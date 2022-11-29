import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListCategory } from '../_models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseUrl = 'https://localhost:7220/api/Category/list-category';
  constructor(private httpClient: HttpClient) { }

  getCategory(): Observable<any>{
    return  this.httpClient.get<ListCategory>(this.baseUrl);
  }
}
