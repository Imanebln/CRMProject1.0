import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { Account } from '../Pages/accounts/account.model';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  AccountKeys: string[] = [
    'accountId',
    'name',
    'websiteUrl',
    'description',
    'fax',
  ];
  apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAccounts() {
    return this.http.get<Account[]>(this.apiUrl + 'Accounts');
  }

  updateAccount(account: Account): Observable<any> {
    return this.http.put<Account>(
      this.apiUrl + 'Accounts/UpdateAccount',
      account
    );
  }

  mapToAccount(obj: any): Account {
    let account: any = <Account>{};
    this.AccountKeys.forEach((key) => (account[key] = obj[key]));
    return account;
  }
  mapToAccounts(objs: any[]): Account[] {
    return objs.map((e) => this.mapToAccount(e)) as Account[];
  }
}
