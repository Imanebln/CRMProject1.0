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
  password: Password = <Password>{};
  token: string;

  constructor(private authService: AuthServiceService) {}

  ngOnInit(): void {
    //passwordForm
    this.passwordForm = new FormGroup({
      password: new FormControl(null, [Validators.required]),
      confirmpassword: new FormControl(null, [Validators.required]),
    });
  }

  onSubmit() {
    this.authService
      .signUp(this.passwordForm.value)
      .subscribe((value) => console.log(value));
  }
}
