import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { CartComponent } from './pages/cart/cart.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { AddProductComponent } from './pages/myshop/add-product/add-product.component';
import { MyshopComponent } from './pages/myshop/myshop.component';
import { CkeditorComponent} from './pages/myshop/ckeditor/ckeditor.component';
import { DefaultComponent } from './layouts/default/default.component';
// import { authGuard } from './auth/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: DefaultComponent,
    children: [
      {
        path: '',
        component: HomeComponent
      },
      {
        path: 'cart',
        component: CartComponent
      },
      {
        path: 'my-shop',
        component: MyshopComponent
      },
      {
        path: 'login',
        component: LoginComponent
      },
      {
        path: 'add-product',
        component: AddProductComponent
      },
      {
        path: 'ckeditor',
        component: CkeditorComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
    CKEditorModule],
  exports: [RouterModule, CKEditorModule]
})
export class AppRoutingModule { }
