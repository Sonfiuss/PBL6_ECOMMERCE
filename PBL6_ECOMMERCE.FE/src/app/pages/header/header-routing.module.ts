import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from 'src/app/pages/login/login.component';
import { HomeComponent } from 'src/app/pages/home/home.component';
import { SignupComponent } from 'src/app/pages/signup/signup.component';
import { DetailComponent } from 'src/app/pages/detail/detail.component';
import { FooterComponent } from '../footer/footer.component';
import { HeaderComponent } from './header.component';
import { ProductDetailComponent } from '../home/products/product-detail/product-detail.component';
import { CartComponent } from '../cart/cart.component';
import { OrderComponent } from '../order/order.component';
import { MyaccountComponent } from '../myaccount/myaccount.component';
import { SearchComponent } from '../search/search.component';


const headerRoutes: Routes = [
  {
    path : '',
    component : HeaderComponent ,
    children: [
        {
          path: '',
          component: HomeComponent,
        },
        {
          path: 'product-detail/:id',
          component: ProductDetailComponent,
        },
        {
          path: 'cart',
          component: CartComponent,
        },
        {
          path: 'order',
          component: OrderComponent,
        },
        {
          path :'myaccount',
          component : MyaccountComponent
        },
        {
          path :'search',
          component : SearchComponent
        },
        // {
        //   path: 'login',
        //   component: LoginComponent,
        //   children: [
        //     {
        //       path: 'signup',
        //       component:SignupComponent ,
        //       }
        //     ,
        //     {
        //       path: 'detail/:id',
        //       component: DetailComponent
        //     }
        //   ]
        // }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(headerRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class HeaderRoutingModule { }

