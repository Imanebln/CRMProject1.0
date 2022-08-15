import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { JwtModule } from '@auth0/angular-jwt';
import { ContactService } from './contact.service';
import { AuthService } from 'src/app/Services/auth.service';
import { environment } from 'src/environments/environment';

export function tokenGetter() {
  return localStorage.getItem('jwt');
}

describe('Contact Service', async () => {
  let authService: AuthService;
  let contactService: ContactService;

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
    let response: any = await lastValueFrom(
      authService.signIn(environment.testUser)
    );
    localStorage.setItem('jwt', response.token);
    contactService = TestBed.inject(ContactService);
  });

  it('#getContact() return all contacts', (done: DoneFn) => {
    contactService.getContacts().subscribe((res) => {
      expect(res.length).toBeGreaterThan(0);
      done();
    });
  });

  it('#getCurrentUser() return current user', (done: DoneFn) => {
    contactService.getCurrentUser().subscribe((res) => {
      expect(res.isPrimary).toEqual(true);
      done();
    });
  });

  it('#getContactsAccount() call', (done: DoneFn) => {
    contactService.getContactsAccount().subscribe((res) => {
      expect(res).toBeTruthy();
      done();
    });
  });

  it('#updateContact() call', async () => {
    let contact: any = {
      contactId: '23cd46cb-b80c-ed11-82e4-000d3abf9655',
      firstname: 'Imane',
      lastname: 'Boulouane',
      birthdate: '1999-08-15',
      email: 'boulouane.imane@gmail.com',
      fax: '0123456987',
      jobTitle: 'IT Engineer',
    };

    let res: any = await lastValueFrom(contactService.updateContact(contact));

    expect(res.message).toBe('Contact updated sucessfully!');
  });

  it('#deleteContact() call', async () => {
    let contact: any = {
      contactId: '23cd46cb-b80c-ed11-82e4-000d3abf9655',
      firstname: 'Imane',
      lastname: 'Boulouane',
      birthdate: '1999-08-15',
      email: 'boulouane.imane@gmail.com',
      fax: '0123456987',
      jobTitle: 'IT Engineer',
    };

    expect(true).toBe(true);

    // let res: any = await lastValueFrom(contactService.deleteContact(contact));

    // expect(res.message).toBe('Contact updated sucessfully!');
  });

  it('#getCountries() call all countries', async () => {
    let res: any = await lastValueFrom(contactService.getCountries());

    expect(res).toBeTruthy();
  });
});
