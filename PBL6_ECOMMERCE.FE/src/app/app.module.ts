import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DefaultComponent } from './layouts/default/default.component';
import { HomeComponent } from './pages/home/home.component';
import { DetailComponent } from './pages/detail/detail.component';
import { BannerComponent } from './pages/home/banner/banner.component';
import { ProductComponent } from './pages/home/product/product.component';
import { HeaderComponent } from './pages/header/header.component';
import { BrandComponent } from './pages/brand/brand.component';
import { FooterComponent } from './pages/footer/footer.component';
import { HttpClientModule } from '@angular/common/http';
import { CartComponent } from './pages/cart/cart.component';
import { LoginComponent } from './pages/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MyshopComponent } from './pages/myshop/myshop.component';
import { AddProductComponent } from './pages/myshop/add-product/add-product.component';
import { CkeditorComponent } from './pages/myshop/ckeditor/ckeditor.component';




@NgModule({
  declarations: [
    AppComponent,
    DefaultComponent,
    HomeComponent,
    DetailComponent,
    BannerComponent,
    ProductComponent,
    HeaderComponent,
    BrandComponent,
    FooterComponent,
    CartComponent,
    LoginComponent,
    MyshopComponent,
    AddProductComponent,
    CkeditorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
