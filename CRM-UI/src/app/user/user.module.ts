import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user/user.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { SideBarComponent } from './Components/side-bar/side-bar.component';
import { ProfileComponent } from './Pages/profile/profile.component';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';
import { FormsModule } from '@angular/forms';

import { BarChartComponent } from './Components/Charts/bar-chart/bar-chart.component';
import { LineChartComponent } from './Components/Charts/line-chart/line-chart.component';
import { DoughnutChartComponent } from './Components/Charts/doughnut-chart/doughnut-chart.component';


@NgModule({
  declarations: [
    UserComponent,
    DashboardComponent,
    SideBarComponent,
    ProfileComponent,
    NavBarComponent,
    BarChartComponent,
    LineChartComponent,
    DoughnutChartComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    FormsModule
  ]
})
export class UserModule { }
