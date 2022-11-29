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
    CartComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
