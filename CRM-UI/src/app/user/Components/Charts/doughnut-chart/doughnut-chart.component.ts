import { Component, Input, OnInit } from '@angular/core';
import {
  Chart,
  ChartConfiguration,
  ChartItem,
  registerables,
} from 'node_modules/chart.js';

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
  // listPays: string[] = [
  //   'Africa',
  //   'Asia',
  //   'Europe',
  //   'Latin America',
  //   'North America',
  // ];

  ngOnInit(): void {
    this.createChart();
  }

  createChart(): void {
    //step1
    Chart.register(...registerables);
    //step2
    const data = {
      labels: this.labelsDoughnut,
      datasets: [
        {
          label: this.datasetsLabelsDoughnut,
          backgroundColor: [
            this.datasetsbackgroundColorDoughnut,
            // '#3e95cd',
            // '#8e5ea2',
            // '#3cba9f',
            // '#e8c3b9',
            // '#c45850',
          ],
          data: this.datasetsdataDoughnut,
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
    // console.log(this.chart.data.datasets[0].data[0]);

    // this.listPays = [
    //   'Africa',
    //   'Africa',
    //   'Africa',
    //   'Latin America',
    //   'North America',
    // ];

    // this.chart.data.labels = this.listPays;

    // this.chart.data.datasets[0].data[0] = 100;
    this.chart?.update();
  }
}
