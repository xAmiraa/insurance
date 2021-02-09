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
import { LookupRoutingModule } from './lookup-routing.module';
import { CoreModule } from 'src/@core/core.module';
// Components
import { PolicyInsurersComponent } from './policy-insurer/policy-insurers/policy-insurers.component';
import { AddEditPolicyInsurerComponent } from './policy-insurer/add-edit-policy-insurer/add-edit-policy-insurer.component';
import { IdentificationTypesComponent } from './identification-type/identification-types/identification-types.component';
import { AddEditIdentificationTypeComponent } from './identification-type/add-edit-identification-type/add-edit-identification-type.component';


@NgModule({
  declarations: [
    
  PolicyInsurersComponent,
    
  AddEditPolicyInsurerComponent,
    
  IdentificationTypesComponent,
    
  AddEditIdentificationTypeComponent],
  imports: [
    CoreModule,
    CommonModule,
    LookupRoutingModule,
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
export class LookupModule { }
