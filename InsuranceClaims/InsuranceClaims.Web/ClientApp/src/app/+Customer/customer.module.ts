import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartsModule as chartjsModule } from 'ng2-charts';
import { NgxEchartsModule } from 'ngx-echarts';
import { MorrisJsModule } from 'angular-morris-js';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
// Angualr Matrial
import { MatListModule } from '@angular/material/list';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
// Routing
import { CustomerRoutingModule } from './customer-routing.module';
import { CoreModule } from '../../@core/core.module';
// Components
import { AddCustomerComponent} from './add-customer/add-customer.component'
import {CustomersComponent} from './customers/customers.component'
@NgModule({
  declarations: [
    AddCustomerComponent,
    CustomersComponent
  ],

  imports: [
    CoreModule,
    CommonModule,
    CustomerRoutingModule,
    chartjsModule,
    NgxEchartsModule,
    MorrisJsModule,
    MatIconModule,
    MatButtonModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatTableModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    NgxDatatableModule,
    MatTooltipModule,
    MatExpansionModule,
    MatTabsModule,
    MatListModule,
  ]
})
export class CustomerModule { }
