import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountsComponent } from './Pages/accounts/accounts.component';
import { ContactsComponent } from './Pages/contacts/contacts.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { OpportunitiesComponent } from './Pages/opportunities/opportunities.component';
import { ProfileComponent } from './Pages/profile/profile.component';
import { ContactGuard } from './Services/contact.guard';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  {
    path: '',
    component: UserComponent,
    children: [
      { path: '', redirectTo: 'dashboard' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'profile', component: ProfileComponent },
      {
        path: 'contacts',
        component: ContactsComponent,
        canActivate: [ContactGuard],
      },
      { path: 'accounts', component: AccountsComponent },
      { path: 'opportunities', component: OpportunitiesComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserRoutingModule {}
