import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import {
  Chart,
  ChartConfiguration,
  ChartItem,
  registerables,
} from 'node_modules/chart.js';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css'],
})
export class BarChartComponent implements OnInit {
  //step5
  chartItem: ChartItem = document.getElementById('bar-chart') as ChartItem;
  //input variable:
  @Input() labelsBar: any;
  @Input() datasetsLabelsBar: any;
  @Input() datasetsbackgroundColorBar: any;
  @Input() datasetsdataBar: any;
  @Input() titleText: any;
  chart!: Chart;

  ngOnInit(): void {
    this.createChart();
  }

  createChart(): void {
    //step1
    Chart.register(...registerables);
    //step2
    const data = {
      labels: this.labelsBar,
      datasets: [
        {
          label: this.datasetsLabelsBar,
          backgroundColor: this.datasetsbackgroundColorBar,
          data: this.datasetsdataBar,
          borderRadius: 15,
        },
      ],
    };
    //step3
    const options = {
      responsive: true,
      plugins: {
        legend: {
          title: {
            display: true,
            text: this.titleText,
            color: '#3e95cd',
            font: {
              size: 30,
            },
            align: 'end',
          },
        },
      },
    };
    //step4
    const config: ChartConfiguration = {
      type: 'bar',
      data: data,
      options: options,
    };
    //step6
    this.chart = new Chart('bar-chart', config);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.chart != undefined) {
      this.chart.data.datasets[0].data =
        changes['datasetsdataBar'].currentValue;
      this.chart?.update();
    }
  }
}
