import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CartComponent } from './pages/cart/cart.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { AddProductComponent } from './pages/myshop/add-product/add-product.component';
import { MyshopComponent } from './pages/myshop/myshop.component';
// import { authGuard } from './auth/auth.guard';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'cart',component:CartComponent},
  {path:'login',component:LoginComponent},
  {path:'myshop', component:MyshopComponent
    // canLoad: [authGuard]
  },
  {path:"add-product", component:AddProductComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
