import { Component, Input, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/_services/category.service';
import { ProductService } from 'src/app/_services/product.service';


@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  constructor(
    private categoryService : CategoryService,
    private productService : ProductService,
  ) { }
  public expand = false;
  @Input() categories :any
  namePd : string
  categoryPd : string
  listPdDetail :Array<any> =[]
  description : string

  sizePd : number
  colorPd : string
  amountPd: number
  pricePd : number
  initialPricePd :number
  ngOnInit(): void {
    this.loadCategory()
  }

  loadCategory(){
    this.categoryService.getCategories()
    .subscribe(
      (res) => this.handleGetCategoriesSuccess(res),
      (err) => this.handleGetCategoriesError(err)
    )
  }

  handleGetCategoriesError(err: any){
    console.log(err);
  }
  handleGetCategoriesSuccess(res: any){
    this.categories = res.result.data
  }
  addProduct() {
    const submitData = {
      "id": 0,
      "name": this.namePd ,
      "material": "string",
      "origin": "string",
      "description": "string",
      "status": true,
      "categories": [
       0
      ]
    }
    this.productService.addProduct(submitData)
    .subscribe(
      (res) => this.handleAddPdSuccess(res),
      (err) => this.handleAddPdError(err)
    )


  }
  handleAddPdError(err: any){
    console.log(err);
  }
  handleAddPdSuccess(res: any){
    alert("them san pham thanh cong")
  }


  typeNamePd(e:any){
    this.namePd = e.target.value
  }
  categorySelected(e:any){
    this.categoryPd = e.target.value
  }

  sizeInput(e:any){
    this.sizePd = e.target.value
  }
  colorInput(e:any){
    this.colorPd = e.target.value
  }
  amountInput(e:any){
    this.amountPd = e.target.value
  }
  initialPriceInput(e:any){
    this.initialPricePd = e.target.value
  }
  priceInput(e:any){
    this.pricePd = e.target.value
  }
  addPdDetail(){
    type pdDetail1 = {
      size : number;
      color : string;
      amount:number;
      price :number;
      initialPrice :number;
    }
    let pdDetail = <pdDetail1>{}
    console.log(pdDetail);
    pdDetail.size = this.sizePd;
    pdDetail.color = this.colorPd
    pdDetail.amount = this.amountPd
    pdDetail.initialPrice = this.initialPricePd
    pdDetail.price = this.pricePd
    this.listPdDetail.push(pdDetail)
    console.log(this.listPdDetail);

  }

}
