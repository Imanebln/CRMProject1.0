import { Component, Input, OnInit, Type, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Account } from '../../Pages/accounts/account.model';
import { Contact } from '../../Pages/contacts/contact.model';
import { ContactService } from '../../Services/contact.service';

@Component({
  selector: 'app-table-data',
  templateUrl: './table-data.component.html',
  styleUrls: ['./table-data.component.css'],
})
export class TableDataComponent implements OnInit {
  //input fields
  @Input() ourData: any;
  @Input() typeOfData: string;
  //free style
  displayedColumns: any;
  //you should specifie the type <XXX>
  public dataSource: MatTableDataSource<Account>;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  //info selected
  infoSelected: any;
  isReadonly: boolean = true;
  ourForm: FormGroup;
  action: string = '';

  constructor(private contactService: ContactService) {}
  ngAfterViewInit(): void {
    // console.log('=======> ', this.ourData);

    this.getAllOwners();
  }

  ngOnInit() {
    if (this.typeOfData == 'Contact') {
      // this.dataSource = new MatTableDataSource<Contact>();
      // this.dataSource.data = this.ourData;
      this.displayedColumns = [
        'firstname',
        'lastname',
        'birthdate',
        'email',
        'mobilePhone',
        'jobTitle',
        'details',
        'update',
        'delete',
      ];
    } else if (this.typeOfData == 'Account') {
      this.displayedColumns = [
        'name',
        'websiteUrl',
        'description',
        'fax',
        'details',
        'update',
        'delete',
      ];
    }
  }
  public getAllOwners = () => {
    // this.repoService.getData('api/owner').subscribe((res) => {
    //   this.dataSource.data = res as Owner[];
    // });
    if (this.typeOfData == 'Contact') {
      this.dataSource = new MatTableDataSource<Account>();
      this.dataSource.data = this.ourData;
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    } else if (this.typeOfData == 'Account') {
      this.dataSource = new MatTableDataSource<Account>();
      this.dataSource.data = this.ourData;
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    }

    // console.log('la valeur de datasource', this.dataSource.data);
    // this.dataSource.data = [
    //   {
    //     contactId: '1',
    //     firstname: 'Mohamed',
    //     lastname: 'Benziza',
    //     birthdate: new Date('2000-12-13'),
    //     emailAddress1: 'Benziza@gmail.com',
    //     fax: '050505050505',
    //     jobTitle: 'Eng',
    //   },
    // ];
    // our data
    // this.dataSource.data = [
    //   {
    //     id: '1',
    //     name: 'Benziza',
    //     dateOfBirth: new Date('2000-12-13'),
    //     address: 'Ben',
    //   },
    //   {
    //     id: '2',
    //     name: 'Amine',
    //     dateOfBirth: new Date('2019-01-16'),
    //     address: 'Ben',
    //   },
    //   {
    //     id: '3',
    //     name: 'Mehdi',
    //     dateOfBirth: new Date('2019-01-16'),
    //     address: 'Ben',
    //   },
    //   {
    //     id: '4',
    //     name: 'Ikram',
    //     dateOfBirth: new Date('2019-01-16'),
    //     address: 'Ben',
    //   },
    //   {
    //     id: '5',
    //     name: 'Bilal',
    //     dateOfBirth: new Date('2019-01-16'),
    //     address: 'Ben',
    //   },
    // ];

    //For our date
    // this.dataSource.data = this.dataSource.data.map((e: any) => {
    //   return { ...e, 'Date Of Birth': e.birthdate.toDateString() };
    // });
  };

  public redirectToDetails = (infoSelected: any) => {
    this.infoSelected = infoSelected;
    this.isReadonly = true;
    if (this.typeOfData == 'Contact') {
      this.ourForm = new FormGroup({
        firstname: new FormControl(this.infoSelected.firstname),
        lastname: new FormControl(this.infoSelected.lastname),
        birthdate: new FormControl(this.infoSelected.birthdate),
        email: new FormControl(this.infoSelected.email),
        mobilePhone: new FormControl(this.infoSelected.mobilePhone),
        fax: new FormControl(this.infoSelected.fax),
        jobTitle: new FormControl(this.infoSelected.jobTitle),
        isPrimary: new FormControl(this.infoSelected.isPrimary),
      });
    } else if (this.typeOfData == 'Account') {
    }
    this.action = 'information';
  };
  public redirectToUpdate = (infoSelected: any) => {
    this.infoSelected = infoSelected;
    this.isReadonly = false;
    if (this.typeOfData == 'Contact') {
      this.ourForm = new FormGroup({
        firstname: new FormControl(this.infoSelected.firstname),
        lastname: new FormControl(this.infoSelected.lastname),
        birthdate: new FormControl(this.infoSelected.birthdate),
        email: new FormControl(this.infoSelected.email),
        mobilePhone: new FormControl(this.infoSelected.mobilePhone),
        fax: new FormControl(this.infoSelected.fax),
        jobTitle: new FormControl(this.infoSelected.jobTitle),
        isPrimary: new FormControl(this.infoSelected.isPrimary),
      });
    } else if (this.typeOfData == 'Account') {
    }
    this.action = 'update';
  };
  public redirectToDelete = (infoSelected: any) => {
    this.infoSelected = infoSelected;
    this.action = 'delete';
  };
  public doFilter = (event: any) => {
    this.dataSource.filter = event.target.value.trim().toLocaleLowerCase();
  };
  onSubmit() {
    this.contactService.updateContact(this.ourForm.value).subscribe();
  }
  updateContact() {
    this.onSubmit();
  }
  deleteContact() {
    this.contactService.deleteContact(this.infoSelected.contactId).subscribe();
  }
}
