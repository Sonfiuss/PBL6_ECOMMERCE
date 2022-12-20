import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyShopRoutingModule } from './my-shop-routing.module';
import { MyShopHeaderComponent } from './my-shop-header/my-shop-header.component';


@NgModule({
  declarations: [
    MyShopHeaderComponent
  ],
  imports: [
    CommonModule,
    MyShopRoutingModule
  ]
})
export class MyShopModule { }
