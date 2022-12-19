import { Component, OnInit } from '@angular/core';
import { ChildrenOutletContexts, Router } from '@angular/router';
import { User } from 'src/app/_models/app-user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-myshop',
  templateUrl: './myshop.component.html',
  styleUrls: ['./myshop.component.css']
})
export class MyshopComponent implements OnInit {
  user : User = new User();
  active = false;
  username : any = null;
  token : any;
  myarr : any;
  accountOpen = false;
  constructor(public accountService: AccountService,private router :Router , private contexts: ChildrenOutletContexts) { }
  ngOnInit(): void {
    this.token = localStorage.getItem("userToken");
    console.log(this.token);
    if(this.token != null){
      this.active = true;
    }
    else{
      this.active = true;
    }
  }
  getRouteAnimationData() {
    return this.contexts.getContext('primary')?.route?.snapshot?.data?.['animation'];
  }


}
