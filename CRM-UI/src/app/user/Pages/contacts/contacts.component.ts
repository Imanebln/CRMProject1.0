import { Component, OnInit } from '@angular/core';
import { ContactService } from '../../Services/contact.service';
import { Contact } from './contact.model';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css'],
})
export class ContactsComponent {
  //data of our Owner
  ourData: Contact[];
  //type of data
  typeOfData = 'Contact';

  constructor(private contactService: ContactService) {}

  ngOnInit(): void {
    this.contactService.getContactsAccount().subscribe((value) => {
      this.ourData = value.contacts;
      // console.log(value.contacts);
    });
  }
}
