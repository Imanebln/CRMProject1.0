import { Component, OnInit } from '@angular/core';
import { OpportunityService } from '../../Services/opportunity.service';
import { Opportunity } from './opportunities.model';

@Component({
  selector: 'app-opportunities',
  templateUrl: './opportunities.component.html',
  styleUrls: ['./opportunities.component.css'],
})
export class OpportunitiesComponent implements OnInit {
  //data of our Owner
  ourData: Opportunity[];
  //type of data
  typeOfData = 'Opportunity';

  constructor(private opportunityService: OpportunityService) {}

  ngOnInit(): void {
    this.opportunityService.getOpportunitys().subscribe((value) => {
      this.ourData = value;
      console.log(value);
    });
  }
}
