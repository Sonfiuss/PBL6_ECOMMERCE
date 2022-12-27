import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';
import { User } from 'src/app/_models/app-user';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  user : User  = new User();
  username :any;
  active  = false;
  token  :any ;
  myarr :any;
  accountOpen = false;
  constructor(public accountService: AccountService) {}


  ngOnInit(): void {

    this.token = localStorage.getItem("token");
    console.log(this.token);
    if (this.token != null)
    {
      this.active = true;
    }
    else{
      this.active = false;
    }
    // this.myarr = this.token.split('"');
    // this.username = this.myarr[3];
    // console.log(this.username);
  }
  logout(){
    this.accountService.logout();
    window.location.reload();

  }

}
