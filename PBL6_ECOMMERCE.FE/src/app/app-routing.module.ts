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
import { AddCategoryComponent } from './pages/categories/add-category/add-category.component';
import { EditCategoryComponent } from './pages/categories/edit-category/edit-category.component';
import { MyaccountComponent } from './pages/myaccount/myaccount.component';
import { InfoComponent } from './pages/myaccount/info/info.component';
import { ChangepasswordComponent } from './pages/myaccount/changepassword/changepassword.component';
import { MyaddressComponent } from './pages/myaccount/myaddress/myaddress.component';
import { AuthGuard } from './_guards/auth.guard';

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
      },
      {
        path: 'add-category',
        component: AddCategoryComponent,
        canActivate: [AuthGuard]
      },
      {
        path: 'edit-category/:id',
        component: EditCategoryComponent
      },
      {
        path: 'myaccount',
        component: MyaccountComponent
      },
      {
        path: 'info',
        component: InfoComponent
      },
      {
        path: 'changepassword',
        component: ChangepasswordComponent
      },
      {
        path: 'myaddress',
        component: MyaddressComponent
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
