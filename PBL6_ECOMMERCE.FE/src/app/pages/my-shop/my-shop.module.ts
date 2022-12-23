import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyShopRoutingModule } from './my-shop-routing.module';
import { MyShopHeaderComponent } from './my-shop-header/my-shop-header.component';
import { AddProductComponent } from './add-product/add-product.component';
import { CkeditorComponent } from './ckeditor/ckeditor.component';
import { ViewListProductComponent } from './view-list-product/view-list-product.component';



@NgModule({
  declarations: [
    MyShopHeaderComponent,
    AddProductComponent,
    CkeditorComponent,
    ViewListProductComponent
    
  ],
  imports: [
    CommonModule,
    MyShopRoutingModule,
  ]
})
export class MyShopModule { }
