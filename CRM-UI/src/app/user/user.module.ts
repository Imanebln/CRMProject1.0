import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user/user.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { SideBarComponent } from './Components/side-bar/side-bar.component';
import { ProfileComponent } from './Pages/profile/profile.component';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BarChartComponent } from './Components/Charts/bar-chart/bar-chart.component';
import { LineChartComponent } from './Components/Charts/line-chart/line-chart.component';
import { DoughnutChartComponent } from './Components/Charts/doughnut-chart/doughnut-chart.component';
import { ContactsComponent } from './Pages/contacts/contacts.component';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatNativeDateModule } from '@angular/material/core';
import { TableDataComponent } from './Components/table-data/table-data.component';
import { AccountsComponent } from './Pages/accounts/accounts.component';
import { HeaderTablePipe } from './Components/table-data/header-table.pipe';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AddressFieldPipe } from './Shared/address-field.pipe';

@NgModule({
  declarations: [
    UserComponent,
    DashboardComponent,
    SideBarComponent,
    ProfileComponent,
    NavBarComponent,
    BarChartComponent,
    LineChartComponent,
    DoughnutChartComponent,
    ContactsComponent,
    TableDataComponent,
    AccountsComponent,
    HeaderTablePipe,
    AddressFieldPipe,
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    FormsModule,
    MatTableModule,
    MatIconModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatPaginatorModule,
    MatNativeDateModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule
  ],
})
export class UserModule {}
