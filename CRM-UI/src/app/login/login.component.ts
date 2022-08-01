import { HttpErrorResponse } from '@angular/common/http';
import { ValueConverter } from '@angular/compiler/src/render3/view/template';
import {
  Component,
  ElementRef,
  OnInit,
  ViewChild,
  AfterViewInit,
} from '@angular/core';
import {
  AsyncValidatorFn,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AlertComponent } from '../alert/alert.component';
import { User } from '../Models/User';
import { AuthServiceService } from '../Services/auth-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  isLogin: boolean = true;
  tmp: boolean = true;
  routeName: string = 'login';
  signUpForm!: FormGroup;
  signInForm!: FormGroup;
  user: User = <User>{};

  constructor(
    private authService: AuthServiceService,
    private router: Router
  ) {}

  @ViewChild(AlertComponent) alert: AlertComponent;

  ngOnInit(): void {
    const token = localStorage.getItem('jwt');
    if (token) {
      this.router.navigate(['dashbord']);
    }

    //signUpForm
    this.signUpForm = new FormGroup({
      email: new FormControl(
        null,
        [Validators.required, Validators.email],
        [this.isRestrictedEmails as AsyncValidatorFn]
      ),
    });
    //signInForm
    this.signInForm = new FormGroup({
      email: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  loginTrue() {
    this.isLogin = true;
    this.routeName = 'login';
  }
  loginFalse() {
    this.isLogin = false;
    this.routeName = 'signUp';
  }

  isRestrictedEmails(control: FormControl): Promise<any> | Observable<any> {
    let promise = new Promise((resolve, reject) =>
      setTimeout(() => {
        if (control.value === 'test@test.com') {
          resolve({ emailIsRestricted: true });
        } else {
          resolve(null);
        }
      }, 2000)
    );
    return promise;
  }

  onSubmit() {
    if (this.isLogin == true) {
      // this.user.email = this.signInForm.value.email;
      // this.user.password = this.signInForm.value.password;
      this.authService.signIn(this.signInForm.value).subscribe({
        next: (response: any) => {
          //Alert Suc
          const token = response.token;
          localStorage.setItem('jwt', token);
          this.alert.ShowAlert({
            type: 'success',
            icon: 'circle-exclamation',
            content: 'Welcome',
          });
          setTimeout(() => {
            this.router.navigate(['dashbord']);
          }, 2000);
        },
        error: (err: HttpErrorResponse) => {
          //Alert Err
          this.alert.ShowAlert({
            type: 'warning',
            icon: 'circle-exclamation',
            content: err.error,
          });
        },
      });
    } else if (this.isLogin == false) {
      this.authService.crmVerification(this.signUpForm.value.email).subscribe({
        next: (response: any) => {
          //Alert Suc
          this.alert.ShowAlert({
            type: 'success',
            icon: 'circle-exclamation',
            content: 'Email send to gmail',
          });
        },
        error: (err: HttpErrorResponse) => {
          if (err.status == 500) {
            this.alert.ShowAlert({
              type: 'warning',
              icon: 'circle-exclamation',
              content: 'You Can not reach email in server!',
            });
          } else {
            this.alert.ShowAlert({
              type: 'warning',
              icon: 'circle-exclamation',
              content: err.error,
            });
          }
        },
      });
    }
  }
}
