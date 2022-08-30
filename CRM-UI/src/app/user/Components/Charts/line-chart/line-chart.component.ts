import { Component, Input, OnInit } from '@angular/core';
import {
  Chart,
  ChartConfiguration,
  ChartItem,
  registerables,
} from 'node_modules/chart.js';
@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css'],
})
export class LineChartComponent implements OnInit {
  //step5
  chartItem: ChartItem = document.getElementById('line-chart') as ChartItem;
  //free style
  @Input() labelsLine: any;
  @Input() datasetsLabelsLine: any;
  @Input() datasetsBorderColorLine: any;
  @Input() datasetsdataLine: any;
  @Input() titleText: any;

  ngOnInit(): void {
    this.createChart();
  }

  createChart(): void {
    //step1
    Chart.register(...registerables);
    //step2
    const data = {
      labels: this.labelsLine,
      datasets: [
        {
          data: [86, 114, 106, 106, 107, 111, 133, 221, 783, 2478],
          label: 'Africa',
          borderColor: '#3e95cd',
          fill: false,
        },
      ],
    };
    //step3
    const options = {
      title: {
        display: true,
        text: this.titleText,
      },
    };
    //step4
    const config: ChartConfiguration = {
      type: 'line',
      data: data,
      // options: options,
    };
    //step6
    new Chart('line-chart', config);
  }
}
