import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusinessService } from './business.service';

const API =  'http://103.197.184.184:8080'
const GET_INFO_USER = API + '/api/User/info-current-user';


@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(
    private httpClient: HttpClient,
    private bussiness: BusinessService
    ) { }

  getInfoUser(){
    return this.httpClient.get(GET_INFO_USER, this.bussiness.getRequestOptions())
  }
}
