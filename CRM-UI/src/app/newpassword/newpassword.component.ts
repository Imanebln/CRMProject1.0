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
    this.passwordModel.token = 'L6DMKBQ6K63OAK3VYQZRJJKDX6YXNC6S';
    this.passwordModel.email = 'heriberto@northwindtraders.com';
    this.passwordModel.password = this.passwordForm.value.password;

    this.authService
      .signUp(this.passwordModel)
      .subscribe((value) => console.log(value));
  }
}
