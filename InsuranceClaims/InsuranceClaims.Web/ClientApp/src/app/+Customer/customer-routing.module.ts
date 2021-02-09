import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NgxPermissionsGuard } from 'ngx-permissions';
// Components
import { CustomersComponent } from './customers/customers.component'
import {AddCustomerComponent} from './add-customer/add-customer.component'
const routes: Routes = [
  {
    path: 'customers',
    component: CustomersComponent,
    // canActivate: [NgxPermissionsGuard],
    // data: { permissions: { only: ['Manager'], redirectTo: '/dashboard' } },
  },
  {
    path: 'add-customer',
    component: AddCustomerComponent,
    // canActivate: [NgxPermissionsGuard],
    // data: { permissions: { only: ['Manager'], redirectTo: '/dashboard' } },
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
