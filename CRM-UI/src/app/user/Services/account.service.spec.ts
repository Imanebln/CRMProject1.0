import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { JwtModule } from '@auth0/angular-jwt';
import { ContactService } from './contact.service';
import { AuthService } from 'src/app/Services/auth.service';
import { AccountService } from './account.service';
import { environment } from 'src/environments/environment';

export function tokenGetter() {
  return localStorage.getItem('jwt');
}

describe('AccountService', async () => {
  let authService: AuthService;
  let accountService: AccountService;

  beforeEach(async () => {
    jasmine.DEFAULT_TIMEOUT_INTERVAL = 10000;
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
    let response: any = await lastValueFrom(
      authService.signIn(environment.testUser)
    );
    localStorage.setItem('jwt', response.token);
    accountService = TestBed.inject(AccountService);
  });

  it('#getAccounts() return all accounts', (done: DoneFn) => {
    accountService.getAccounts().subscribe((res) => {
      expect(res.length).toBeGreaterThan(0);
      done();
    });
  });

  it('#updateAccount() call', async () => {
    let account: any = {
      accountId: 'c0ae4eef-b80c-ed11-82e4-000d3abf9656',
      description:
        'This a trash company that can do absolutely nothing,Yeah :)',
      fax: '62107536818',
      name: '3agona IT',
      websiteUrl: 'https://www.imaneit7.com',
    };

    try {
      let res: any = await lastValueFrom(accountService.updateAccount(account));
    } catch (e: any) {
      console.log(e);
    }

    // expect(res.message).toBeTruthy();
  });
});
