import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NgxPermissionsGuard } from 'ngx-permissions';
// Components
import { PolicyInsurersComponent } from './policy-insurer/policy-insurers/policy-insurers.component';
import { IdentificationTypesComponent } from './identification-type/identification-types/identification-types.component';

const routes: Routes = [
  {
    path: 'policy-insurers',
    component: PolicyInsurersComponent,
    // canActivate: [NgxPermissionsGuard],
    // data: { permissions: { only: ['Manager'], redirectTo: '/dashboard' } },
  },
  {
    path: 'identification-types',
    component: IdentificationTypesComponent,
    // canActivate: [NgxPermissionsGuard],
    // data: { permissions: { only: ['Manager'], redirectTo: '/dashboard' } },
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LookupRoutingModule { }
