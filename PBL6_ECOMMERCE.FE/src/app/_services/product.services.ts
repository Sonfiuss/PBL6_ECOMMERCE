import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserToken } from 'src/app/_models/app-user';


@Injectable({
  providedIn: 'root'
})
export class productService {

  constructor(private httpClient: HttpClient) { }

  getPosts() {
    return this.httpClient
    .get('https://jsonplaceholder.typicode.com/posts')
  }
}
