import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Account } from '../Pages/accounts/account.model';
import { Contact } from '../Pages/contacts/contact.model';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  ContactKeys: string[] = [
    'contactId',
    'firstname',
    'lastname',
    'birthdate',
    'email',
    'mobilePhone',
    'jobTitle',
  ];
  apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getContacts() {
    return this.http.get<Contact[]>(this.apiUrl + 'Contacts');
  }

  getCurrentUser() {
    return this.http.get<Contact>(this.apiUrl + 'Auth/GetCurrentUser');
  }

  getContactsAccount() {
    return this.http.get<Account>(this.apiUrl + 'Auth/GetContactsAccount');
  }

  updateContact(contact: Contact): Observable<any> {
    return this.http.put<Contact>(
      this.apiUrl + 'Contacts/UpdateContact',
      contact
    );
  }

  deleteContact(contactId: string): Observable<any> {
    return this.http.delete(
      this.apiUrl + 'Contacts/DeleteContact?id=' + contactId
    );
  }

  mapToContact(obj: any): Contact {
    let contact: any = <Contact>{};
    this.ContactKeys.forEach((key) => (contact[key] = obj[key]));
    return contact;
  }
  mapToContacts(objs: any[]): Contact[] {
    return objs.map((e) => this.mapToContact(e)) as Contact[];
  }
}
