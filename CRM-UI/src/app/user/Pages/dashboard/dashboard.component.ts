import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ContactService } from '../../Services/contact.service';
import { OpportunityService } from '../../Services/opportunity.service';
import { Contact } from '../contacts/contact.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  closeProbability: number[];
  //Bar Chart start
  datasetsdataBar: number[] = [];
  labelsBar: string[] = [];
  datasetsLabelsBar = 'Estimated opportunities (percentage)';
  datasetsbackgroundColorBar = ['#602b70', '#b23660'];
  //Bar Chart end
  labelsLine: string[] = [];
  datasetsLabelsLine: number[] = [];
  datasetsBorderColorLine = '#3e95cd';
  datasetsdataLine = 'Africa';
  titleText = 'World population per region (in millions)';
  //Line Chart Start
  //Line Chart End
  //data of our Owner
  ourData: Contact[];
  //type of data
  typeOfData = 'Contact';

  constructor(
    private router: Router,
    private contactService: ContactService,
    private opportunityService: OpportunityService
  ) {}

  toContacts() {
    this.router.navigate(['contacts']);
  }

  ngOnInit(): void {
    this.contactService.getContactsAccount().subscribe((value) => {
      this.ourData = value.contacts;
    });
    this.opportunityService.getOpportunitys().subscribe((value) => {
      value.forEach((v) => this.datasetsdataBar.push(v.closeProbability));
      value.forEach((v) => this.labelsBar.push(v.name));
      value.forEach((v) => {
        this.datasetsLabelsLine.push(v.estimatedValue);
      });
      value.forEach((v) => this.labelsLine.push(v.name));
    });
  }
}
