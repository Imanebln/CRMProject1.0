import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
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
  //type of data
  typeOfData = 'Account';
  //Logic
  isReadOnly: boolean = true;
  buttonText: string = 'Edit';
  //Form
  accountForm;
  //isPrimary
  isPrimary: boolean = false;

  constructor(
    private contactService: ContactService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.contactService.getContactsAccount().subscribe((value) => {
      this.ourData = value;
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
    // TODO: Use EventEmitter with form value
    this.accountService.updateAccount(this.accountForm.value).subscribe();
    // console.log(this.accountForm.value);
  }
}
