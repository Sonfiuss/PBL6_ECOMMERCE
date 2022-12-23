import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DefaultComponent } from './layouts/default/default.component';
import { HomeComponent } from './pages/home/home.component';
import { DetailComponent } from './pages/detail/detail.component';
import { BannerComponent } from './pages/home/banner/banner.component';
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
import { CategoryComponent } from './pages/home/category/category.component';
import { AddCategoryComponent } from './pages/categories/add-category/add-category.component';
import { EditCategoryComponent } from './pages/categories/edit-category/edit-category.component';
import { ProductComponent } from './pages/home/products/product/product.component';
import { ProductDetailComponent } from './pages/home/products/product-detail/product-detail.component';
import { MyaccountComponent } from './pages/myaccount/myaccount.component';
import { InfoComponent } from './pages/myaccount/info/info.component';
import { ChangepasswordComponent } from './pages/myaccount/changepassword/changepassword.component';
import { MyaddressComponent } from './pages/myaccount/myaddress/myaddress.component';
import { SignupComponent } from './pages/signup/signup.component';
import { OrderComponent } from './pages/order/order.component';
import { MyorderComponent } from './pages/myaccount/myorder/myorder.component';
import { ShopViewComponent } from './pages/shop-view/shop-view.component';
import { AdminComponent } from './pages/admin/admin.component';


@NgModule({
  declarations: [
    AppComponent,
    DefaultComponent,
    HomeComponent,
    DetailComponent,
    BannerComponent,
    HeaderComponent,
    BrandComponent,
    FooterComponent,
    CartComponent,
    LoginComponent,
    MyshopComponent,
    AddProductComponent,
    CkeditorComponent,
    CategoryComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    ProductComponent,
    ProductDetailComponent,
    EditCategoryComponent,
    MyaccountComponent,
    InfoComponent,
    ChangepasswordComponent,
    MyaddressComponent,
    SignupComponent,
    OrderComponent,
    MyorderComponent,
    ShopViewComponent,
    AdminComponent
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