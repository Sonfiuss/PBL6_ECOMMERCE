import { Component, OnInit,Input } from '@angular/core';
import { HomeService } from 'src/app/_services/home.service';



@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor(
    private homeService : HomeService,
  ) { }

  ngOnInit(): void {
    this.loadAllProduct()
  }

  @Input() productData:any;
  pageSize =6;
  page = 1 ;
  key = 'price';
  reverse = false;
  category = "quần áo";

  handlePageChange(event : any) {
    this.page = event;
  }


  onChange(deviceValue: any) {
    if (deviceValue === 'price-low-to-high'){
      this.reverse =false;
    }
    else{
      this.reverse = true
    }
  }
  loadAllProduct(){
    this.homeService.search(1)
    .subscribe(
      (res) => this.handleGetProductSuccess(res),
      (err) => this.handleGetProductError(err)
    )
  }

  handleGetProductError(err: any){
    console.log(err)
  }

  handleGetProductSuccess(res: any){
    this.productData = res.result.data
    console.log(this.productData)
  }
}
