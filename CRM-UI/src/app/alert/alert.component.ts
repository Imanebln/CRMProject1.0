import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  ViewChild,
  ElementRef,
} from '@angular/core';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css'],
})
export class AlertComponent implements OnInit {
  ngOnInit(): void {}
  Alert: AlertModel = <AlertModel>{
    type: 'success',
    icon: 'circle-check',
  };
  style: any = {
    border: '2px solid var(--alert-success-border)',
    display: 'none',
  };
  loading: any = {};
  timeout: number = 3000;

  ShowAlert = (alertmodel: AlertModel) => {
    this.Alert.content = alertmodel.content;
    if (alertmodel.icon != null) this.Alert.icon = alertmodel.icon;
    if (alertmodel.type != null) this.Alert.type = alertmodel.type;

    this.style.border = `2px solid var(--alert-${alertmodel.type}-border)`;
    this.style.animation = 'DisplayAnimation 600ms ease';
    this.style.display = 'block';

    this.loading.animation = 'LoadingAnimation 3s linear';
    setTimeout(() => {
      this.style.animation = 'CollapseAnimation 600ms ease';
      setTimeout(() => {
        this.style.display = 'none';
      }, 600);
    }, this.timeout);
  };

  CloseAlert = () => {
    this.style.animation = 'CollapseAnimation 600ms ease';
    setTimeout(() => {
      this.style.display = 'none';
    }, 600);
  };
}

export interface AlertModel {
  type?: string;
  icon?: string;
  content: string;
}
