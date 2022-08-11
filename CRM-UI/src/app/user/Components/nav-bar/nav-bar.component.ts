import { Component, Input, OnInit } from '@angular/core';
import { Contact } from '../../Pages/contacts/contact.model';
import { ContactService } from '../../Services/contact.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  public User : Contact;
  constructor(private contactService : ContactService) { 
    this.contactService.getCurrentUser().subscribe(user => this.User = user)
  }

  ngOnInit(): void {
  }

}
