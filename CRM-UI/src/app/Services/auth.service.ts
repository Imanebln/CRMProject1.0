import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Password } from '../Models/Password';
import { User } from '../Models/User';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  crmVerification(email: string) {
    const params: HttpParams = new HttpParams({
      fromObject: {
        email,
      },
    });
    return this.http.get(this.apiUrl + 'Auth/CRMVerification?' + params);
  }
  signIn(user: any) {
    return this.http.post(this.apiUrl + 'Auth/SignIn', user);
  }
  signUp(password: Password) {
    return this.http.post(this.apiUrl + 'Auth/SignUp', password);
  }
  recoverPassword(email: any) {
    return this.http.post(
      this.apiUrl + 'Auth/RecoverPassword?email=' + email,
      {}
    );
  }
}
