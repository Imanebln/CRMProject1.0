import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ContactService } from '../../Services/contact.service';
import { Contact } from '../contacts/contact.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  //Bar Chart start
  datasetsdataBar = [2478, 5267, 734, 784, 433];
  labelsBar = ['Africa', 'Asia', 'Europe', 'Latin America', 'North America'];
  datasetsLabelsBar = 'Population (millions)';
  datasetsbackgroundColorBar = [
    '#602b70',
    '#b23660',
    // '#3e95cd',
    // '#8e5ea2',
    // '#3cba9f',
    // '#e8c3b9',
    // '#c45850',
  ];
  //Bar Chart end
  labelsLine = [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050];
  datasetsLabelsLine = [86, 114, 106, 106, 107, 111, 133, 221, 783, 2478];
  datasetsBorderColorLine = '#3e95cd';
  datasetsdataLine = 'Africa';
  titleText = 'World population per region (in millions)';
  //Line Chart Start
  //Line Chart End
  //data of our Owner
  ourData: Contact[];
  //type of data
  typeOfData = 'Contact';

  constructor(private router: Router, private contactService: ContactService) {}

  toContacts() {
    this.router.navigate(['contacts']);
  }

  ngOnInit(): void {
    this.contactService.getContactsAccount().subscribe((value) => {
      this.ourData = value.contacts;
    });
  }
}
