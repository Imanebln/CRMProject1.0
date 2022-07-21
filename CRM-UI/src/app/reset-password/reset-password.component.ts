import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Password } from '../Models/Password';
import { AuthServiceService } from '../Services/auth-service.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
  resetpasswordForm!: FormGroup;

  constructor(private authService: AuthServiceService) {}

  ngOnInit(): void {
    //resetpasswordForm
    this.resetpasswordForm = new FormGroup({
      email: new FormControl(null, [Validators.required]),
    });
  }

  onSubmit() {
    console.log(this.resetpasswordForm.value);
  }
}
