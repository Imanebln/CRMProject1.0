import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { JwtModule } from '@auth0/angular-jwt';
import { ContactService } from './contact.service';
import { AuthServiceService } from 'src/app/Services/auth-service.service';
import { AccountService } from './account.service';

export function tokenGetter() {
  return localStorage.getItem('jwt');
}

fdescribe('AccountService', async () => {
  let authService: AuthServiceService;
  let accountService: AccountService;

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
    authService = TestBed.inject(AuthServiceService);
    let response: any = await lastValueFrom(
      authService.signIn({
        email: 'boulouane.imane@gmail.com',
        password: 'Crm123@',
      })
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
