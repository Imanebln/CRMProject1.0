import { ValueConverter } from '@angular/compiler/src/render3/view/template';
import { Component, OnInit } from '@angular/core';
import {
  AsyncValidatorFn,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
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

  ngOnInit(): void {
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

      console.log(this.signInForm.value);

      this.authService
        .signIn(this.signInForm.value)
        .subscribe((value) => console.log(value));
    } else if (this.isLogin == false) {
      this.authService
        .crmVerification(this.signUpForm.value.email)
        .subscribe((value) => console.log(value));
      console.log(this.signUpForm.value.email);
    }
  }
}
