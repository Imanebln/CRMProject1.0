import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToasterComponent } from '../../Components/toaster/toaster.component';
import { ContactDetails } from '../../Models/ContactDetails.models';
import { AccountService } from '../../Services/account.service';
import { ContactService } from '../../Services/contact.service';
import { Contact } from '../contacts/contact.model';
import { Account } from './account.model';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css'],
})
export class AccountsComponent implements OnInit {
  //data of our Owner
  ourData: Account;
  primaryContact : Contact;
  //type of data
  typeOfData = 'Account';
  //Logic
  isReadOnly: boolean = true;
  buttonText: string = 'Edit';
  //Form
  accountForm;
  //isPrimary
  isPrimary: boolean = false;
  ImageUrl : string;

  //ContactShowed
  contactShowed : boolean = false ;

  @ViewChild(ToasterComponent)
  toaster : ToasterComponent;

  constructor(
    private contactService: ContactService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.contactService.getContactsAccount().subscribe((value) => {
      this.ourData = value;
      this.ImageUrl = 'data:image/png;base64,' + this.ourData.imageUrl;
      this.primaryContact = this.ourData.primaryContact;
      
      this.accountForm = new FormGroup({
        accountId: new FormControl(this.ourData.accountId),
        name: new FormControl(this.ourData.name),
        websiteUrl: new FormControl(this.ourData.websiteUrl),
        description: new FormControl(this.ourData.description),
        fax: new FormControl(this.ourData.fax),
      });
      
    });
    this.contactService.getCurrentUser().subscribe((res) => {
      this.isPrimary = res.isPrimary;
    });
  }
  submitForm(event: any) {
    this.onSubmit();
  }
  edit() {
    this.isReadOnly = !this.isReadOnly;
    this.buttonText = this.buttonText == 'Edit' ? 'Save' : 'Edit';
  }

  onSubmit() {
    this.accountService.updateAccount(this.accountForm.value).subscribe(res =>{
      this.toaster.addToast({
        icon: 'check',
        content: 'Account Updated Successfully !',
        type: 'success'
      })
    });
    window.location.reload();
  }
  showContacts = () =>{
    let i = 0;
    if (!this.contactShowed) {
      document.querySelectorAll('.contacts').forEach((elm: any) => {
        setTimeout(() => {
          elm.style.display = 'block';
          elm.style.animation = 'ShowContact 600ms ease-out';
        }, i * 200);
        i++;
      });
    } else {
      document.querySelectorAll('.contacts').forEach((elm: any) => {
        setTimeout(() => {
          elm.style.animation = 'HideContact 600ms ease-out';
          setTimeout(() => {
            elm.style.display = 'none';
          }, 300);
        }, i * 200);
        i++;
      });
    }
    this.contactShowed = !this.contactShowed;
  };  
}
 
