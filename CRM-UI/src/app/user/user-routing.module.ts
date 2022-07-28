import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { ProfileComponent } from './Pages/profile/profile.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  {path: '', component: UserComponent, children:[
    {path: '', redirectTo: 'dashboard'},
    {path: 'dashboard', component: DashboardComponent},
    {path: 'profile', component: ProfileComponent}
  ]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
