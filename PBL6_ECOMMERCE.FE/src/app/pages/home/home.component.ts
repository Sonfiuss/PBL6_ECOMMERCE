import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/_services/category.service';
import { Category } from 'src/app/_models/category';
import { ListCategory } from 'src/app/_models/category';
import { Data } from 'src/app/_models/category';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor( private category :CategoryService) { }
  categoryData:any;
  photoData:any;
  categoryList: ListCategory = new ListCategory();
  list:any;
  banners = [{id:1},{id:2}]
  arr : Array<Category> =new Array();
  data : Data =new Data();
  ngOnInit(): void {
    this.category.getCategory().subscribe(res =>{
      console.log(res);
      this.categoryList = res;
      console.log(this.categoryList);
      // this.data = this.categoryList.result;
      console.log(this.arr);
    });
    console.log(this.arr);
  }

}
