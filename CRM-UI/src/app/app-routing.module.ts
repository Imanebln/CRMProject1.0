import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { NewpasswordComponent } from './newpassword/newpassword.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'newpassword', component: NewpasswordComponent },
  { path: 'resetpassword', component: ResetPasswordComponent },
  {path: 'user', loadChildren: () => import('./user/user.module').then(m => m.UserModule), canActivate:[AuthGuard]},
  { path: '', redirectTo: '/user/dashboard', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
