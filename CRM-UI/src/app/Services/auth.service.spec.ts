import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthService } from './auth.service';
import { environment } from 'src/environments/environment';

export function tokenGetter() {
  return localStorage.getItem('jwt');
}

describe('AuthService', async () => {
  let authService: AuthService;

  beforeEach(async () => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
        JwtModule.forRoot({
          config: {
            tokenGetter: tokenGetter,
            allowedDomains: ['localhost:7270'],
            disallowedRoutes: [],
          },
        }),
      ],
    });
    TestBed.inject(HttpClient);
    authService = TestBed.inject(AuthService);
  });

  it('#signIn() call', async () => {
    let response: any = await lastValueFrom(
      authService.signIn(environment.testUser)
    );
    expect(response.isAuthenticated).toBe(true);
    // localStorage.setItem('jwt', response.token);
  });

  it('#crmVerification() call', async () => {
    try {
      let response: any = await lastValueFrom(
        authService.crmVerification('notexistedemail@gmail.com')
      );
    } catch (e: any) {
      expect(e.status).toBe(400);
    }
  });

  // it('#recoverPassword() call', async () => {
  //   try {
  //     let response: any = await lastValueFrom(
  //       authService.recoverPassword('notexistedemail@gmail.com')
  //     );
  //   } catch (e: any) {
  //     expect(e.status).toBe(400);
  //   }
  // });

  // it('#signUp() call', async () => {
  //   try {
  //     let response: any = await lastValueFrom(
  //       authService.signUp(new Password{})
  //     );
  //   } catch (e: any) {
  //     expect(e.status).toBe(400);
  //   }
  // });
});
