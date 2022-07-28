import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user/user.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { SideBarComponent } from './Components/side-bar/side-bar.component';
import { ProfileComponent } from './Pages/profile/profile.component';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';


@NgModule({
  declarations: [
    UserComponent,
    DashboardComponent,
    SideBarComponent,
    ProfileComponent,
    NavBarComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule
  ]
})
export class UserModule { }
