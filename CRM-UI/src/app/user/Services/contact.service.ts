import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
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
    'emailAddress1',
    'fax',
    'jobTitle',
  ];
  apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getContacts() {
    return this.http.get<Contact[]>(this.apiUrl + 'Contacts');
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
