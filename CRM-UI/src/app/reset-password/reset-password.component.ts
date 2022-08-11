import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertComponent } from '../alert/alert.component';
import { Password } from '../Models/Password';
import { AuthService } from '../Services/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
  resetpasswordForm!: FormGroup;
  @ViewChild(AlertComponent) alert: AlertComponent;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    //resetpasswordForm
    this.resetpasswordForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
    });
  }

  onSubmit() {
    this.authService
      .recoverPassword(this.resetpasswordForm.value.email)
      .subscribe({
        next: (response: any) => {
          //Alert Suc
          this.alert.ShowAlert({
            type: 'success',
            icon: 'circle-exclamation',
            content: 'Password is confirmed',
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
  }
}
