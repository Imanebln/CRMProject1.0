import { JsonPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  AsyncValidatorFn,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Password } from '../Models/Password';
import { User } from '../Models/User';
import { AuthServiceService } from '../Services/auth-service.service';

@Component({
  selector: 'app-newpassword',
  templateUrl: './newpassword.component.html',
  styleUrls: ['./newpassword.component.css'],
})
export class NewpasswordComponent implements OnInit {
  passwordForm!: FormGroup;
  passwordModel: Password = <Password>{};

  constructor(private authService: AuthServiceService) {}

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

    this.authService
      .signUp(this.passwordModel)
      .subscribe((value) => console.log(value));
  }
}
