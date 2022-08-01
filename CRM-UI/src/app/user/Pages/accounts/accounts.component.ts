import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../Services/account.service';
import { Account } from './account.model';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css'],
})
export class AccountsComponent implements OnInit {
  //data of our Owner
  ourData: Account[];
  //type of data
  typeOfData = 'Account';

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.accountService.getAccounts().subscribe((value) => {
      this.ourData = value;
    });
  }
}
