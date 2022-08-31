import { Component, Input, OnInit } from '@angular/core';
import {
  Chart,
  ChartConfiguration,
  ChartItem,
  registerables,
} from 'node_modules/chart.js';
import { OpportunityService } from 'src/app/user/Services/opportunity.service';

@Component({
  selector: 'app-doughnut-chart',
  templateUrl: './doughnut-chart.component.html',
  styleUrls: ['./doughnut-chart.component.css'],
})
export class DoughnutChartComponent implements OnInit {
  //step5
  chartItem: ChartItem = document.getElementById('doughnut-chart') as ChartItem;
  //free style
  chart!: Chart;
  @Input() labelsDoughnut: any;
  @Input() datasetsLabelsDoughnut: any;
  @Input() datasetsbackgroundColorDoughnut: any;
  @Input() datasetsdataDoughnut: any;
  @Input() titleText: any;
  listStatus: string[] = ['Open', 'Won', 'Close'];
  open: number = 0;
  won: number = 0;
  close: number = 0;

  constructor(private opportunityService: OpportunityService) {}

  ngOnInit(): void {
    this.opportunityService.getOpportunitys().subscribe((value) => {
      value.forEach((v) => {
        if (v.statuscode == 1) {
          this.open += 1;
        } else if (v.statuscode == 3) {
          this.won += 1;
        } else if (v.statuscode == 4) {
          this.close += 1;
        }
      });
      this.createChart();
    });
  }

  createChart(): void {
    //step1
    Chart.register(...registerables);
    //step2
    const data = {
      labels: this.listStatus,
      datasets: [
        {
          label: 'Won/Open/Close',
          backgroundColor: ['#1ab9c5', '#5b38c6', '#3e95cd'],
          data: [this.open, this.won, this.close],
        },
      ],
    };
    //step3
    const options = {
      responsive: true,
      plugins: {
        title: {
          display: true,
          text: this.titleText,
        },
      },
    };
    //step4
    const config: ChartConfiguration = {
      type: 'doughnut',
      data: data,
      options: options,
    };
    //step6
    this.chart = new Chart('doughnut-chart', config);
  }

  changeCountry() {
    this.chart?.update();
  }
}
