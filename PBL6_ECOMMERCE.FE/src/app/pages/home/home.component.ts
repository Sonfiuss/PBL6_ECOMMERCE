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

  constructor(
    private categoryService :CategoryService
  ) { }

  categories:Array<any> = []
  banners = [{id:1},{id:2}]
  arr : Array<Category> =new Array();
  data : Data =new Data();

  ngOnInit(): void {
    this.loadHome()
  }

  loadHome(){
    this.categoryService.getCategories()
    .subscribe(
      (res) => this.handleGetCategorySuccess(res),
      (err) => this.handleGetCategoryError(err)
    )

  }

  handleGetCategoryError(err: any){
    console.log(err)
  }
  handleGetCategorySuccess(res: any){
    this.categories = res.result.data
    console.log(res)
  }
  onDeleteCategoryEvent(categoryId:number){
    console.log('Delete Category - ' + categoryId)
    this.loadHome()
  }

}
