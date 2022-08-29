import { CdkColumnDef } from '@angular/cdk/table';
import { Component, Input, OnInit, Type, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Account } from '../../Pages/accounts/account.model';
import { Contact } from '../../Pages/contacts/contact.model';
import { Opportunity } from '../../Pages/opportunities/opportunities.model';
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
  displayedColumns: any;
  public dataSource: MatTableDataSource<Opportunity>;
  public dataSource1: MatTableDataSource<Account>;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  //info selected
  infoSelected: any;
  isReadonly: boolean = true;
  ourForm: FormGroup;
  action: string = '';

  constructor(private contactService: ContactService) {}

  ngAfterViewInit(): void {
    this.getAllOwners();
  }

  ngOnInit() {
    const adminCrud = ['update', 'delete'];

    const token = localStorage.getItem('jwt');
    const tokenInfo = this.contactService.getDecodedAccessToken(
      localStorage.getItem('jwt')
    );
    const userRole = tokenInfo.roles;

    if (this.typeOfData == 'Contact') {
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
    } else if (this.typeOfData == 'Opportunity') {
      this.displayedColumns = [
        'email',
        'totalAmount',
        'createdOn',
        'description',
        'currentSituation',
        'proposedSolution',
        'details',
      ];
    }
  }

  public getAllOwners = () => {
    if (this.typeOfData == 'Contact') {
      this.dataSource1 = new MatTableDataSource<Account>();
      this.dataSource1.data = this.ourData;
      this.dataSource1.sort = this.sort;
      this.dataSource1.paginator = this.paginator;
    } else if (this.typeOfData == 'Opportunity') {
      this.dataSource = new MatTableDataSource<Opportunity>();
      this.dataSource.data = this.ourData;
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    }
  };

  public redirectToDetails = (infoSelected: any) => {
    this.infoSelected = infoSelected;
    this.isReadonly = true;
    if (this.typeOfData == 'Contact') {
      this.ourForm = new FormGroup({
        contactId: new FormControl(this.infoSelected.contactId),
        firstname: new FormControl(this.infoSelected.firstname),
        lastname: new FormControl(this.infoSelected.lastname),
        birthdate: new FormControl(this.infoSelected.birthdate),
        email: new FormControl(this.infoSelected.email),
        mobilePhone: new FormControl(this.infoSelected.mobilePhone),
        fax: new FormControl(this.infoSelected.fax),
        jobTitle: new FormControl(this.infoSelected.jobTitle),
      });
    } else if (this.typeOfData == 'Opportunity') {
      this.ourForm = new FormGroup({
        opportunityId: new FormControl(this.infoSelected.opportunityId),
        name: new FormControl(this.infoSelected.name),
        stepName: new FormControl(this.infoSelected.stepName),
        estimatedClosedate: new FormControl(
          this.infoSelected.estimatedClosedate
        ),
        closeProbability: new FormControl(this.infoSelected.closeProbability),
        estimatedValue: new FormControl(this.infoSelected.estimatedValue),
        description: new FormControl(this.infoSelected.description),
        email: new FormControl(this.infoSelected.email),
      });
    }
    this.action = 'information';
    console.log(this.ourForm.value);
  };

  public redirectToUpdate = (infoSelected: any) => {
    this.infoSelected = infoSelected;
    this.isReadonly = false;
    if (this.typeOfData == 'Contact') {
      this.ourForm = new FormGroup({
        contactId: new FormControl(this.infoSelected.contactId),
        firstname: new FormControl(this.infoSelected.firstname),
        lastname: new FormControl(this.infoSelected.lastname),
        birthdate: new FormControl(this.infoSelected.birthdate),
        email: new FormControl(this.infoSelected.email),
        mobilePhone: new FormControl(this.infoSelected.mobilePhone),
        fax: new FormControl(this.infoSelected.fax),
        jobTitle: new FormControl(this.infoSelected.jobTitle),
      });
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
