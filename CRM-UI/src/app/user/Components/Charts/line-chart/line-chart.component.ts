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
          data: this.datasetsdataLine,
          label: this.datasetsLabelsLine,
          borderColor: this.datasetsBorderColorLine,
          fill: false,
        },
        // {
        //   data: [282, 350, 411, 502, 635, 809, 947, 1402, 3700, 5267],
        //   label: 'Asia',
        //   borderColor: '#8e5ea2',
        //   fill: false,
        // },
        // {
        //   data: [168, 170, 178, 190, 203, 276, 408, 547, 675, 734],
        //   label: 'Europe',
        //   borderColor: '#3cba9f',
        //   fill: false,
        // },
        // {
        //   data: [40, 20, 10, 16, 24, 38, 74, 167, 508, 784],
        //   label: 'Latin America',
        //   borderColor: '#e8c3b9',
        //   fill: false,
        // },
        // {
        //   data: [6, 3, 2, 2, 7, 26, 82, 172, 312, 433],
        //   label: 'North America',
        //   borderColor: '#c45850',
        //   fill: false,
        // },
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
