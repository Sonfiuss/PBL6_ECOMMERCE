import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit {

  avtUrl ='./assets/img/userAvatar/Avatar.jpg';
  constructor() { }

  ngOnInit(): void {
  }


  onSelectFile(e:any){
    if(e.target.files){
      var reader = new FileReader();
      reader.readAsDataURL(e.target.files[0]);
      reader.onload=(event:any)=>{
        this.avtUrl=event.target.result;
        // console.log(event.target.result);

      }
    }
  }
}
