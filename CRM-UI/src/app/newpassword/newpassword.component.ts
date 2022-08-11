import { JsonPipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import {
  AbstractControl,
  AsyncValidatorFn,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AlertComponent } from '../alert/alert.component';
import { Password } from '../Models/Password';
import { AuthService } from '../Services/auth.service';

@Component({
  selector: 'app-newpassword',
  templateUrl: './newpassword.component.html',
  styleUrls: ['./newpassword.component.css'],
})
export class NewpasswordComponent implements OnInit {
  passwordForm!: FormGroup;
  passwordModel: Password = <Password>{};
  @ViewChild(AlertComponent) alert: AlertComponent;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    //passwordForm
    this.passwordForm = new FormGroup({
      password: new FormControl(null, [Validators.required]),
      confirmpassword: new FormControl(null, [Validators.required]),
    });
  }

  onSubmit() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    this.passwordModel.token = urlParams.get('token') + '';
    this.passwordModel.email = urlParams.get('email') + '';
    this.passwordModel.password = this.passwordForm.value.password;

    this.authService.signUp(this.passwordModel).subscribe({
      next: (response: any) => {
        //Alert Suc
        this.alert.ShowAlert({
          type: 'success',
          icon: 'circle-exclamation',
          content: 'You have a new password',
        });
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

    setTimeout(() => {
      this.router.navigate(['login']);
    }, 2000);
  }
}
