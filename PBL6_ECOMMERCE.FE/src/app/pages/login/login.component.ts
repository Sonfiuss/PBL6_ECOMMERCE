import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from 'src/app/_models/app-user';
import { AccountService } from 'src/app/_services/account.service';
import { Routes, RouterModule, Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user : User  = new User();
  constructor(public accountService: AccountService,private router :Router) { }
  ngOnInit(): void {
  }
  token:any ;
  myarr :any;
  login(){
    this.accountService.login(this.user).subscribe();
    this.token = localStorage.getItem("userToken");
    if (this.token != null)
    {
      this.router.navigateByUrl('');
    }
  }

}
