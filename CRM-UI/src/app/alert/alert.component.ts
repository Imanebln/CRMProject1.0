
import { Component, 
          OnInit,
          Input,
          Output,
          EventEmitter,
          ViewChild,
          ElementRef} from '@angular/core';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})


export class AlertComponent implements OnInit {
//Type of alert that is going to be received
//Type should be either error or success
  @Input() type : 'error' | 'success';
  @Input() messageHeader : string;
  @Input() messageContent : string;
  @Output() onClose = new EventEmitter<any>();
  
  
  //View Child should be in the component in which we want to use it 
 // @ViewChild('alert', { static: false }) divAlert: ElementRef;
  
  
 

  isSuccess : boolean = false;
  isError : boolean = false;
  
  constructor() { }

  ngOnInit(): void { 
  }

  Close(){
    this.onClose.emit()
  }

  //Implement functions that is going to be called in other components 
  //createAlert(type : string,messageHeader : string, messageContent : string){
  createAlert(){
          if (this.type == "error"){
            this.isError = true;
          }
          if(this.type == "success"){
            this.isSuccess = true;
          }
  }


  //TODO : when using this component you need only to add its tag where you wannna
  // be using it and specifying the type of the alert it is only custom 
  

  
  //The following code needs to be added in the component class with a property isAlert and needs to be tested with ngIf 
  
  // onAlertClose(){
  //   this.isAlert = false;
  // }
  // onAlertOpen(){
  //   this.isAlert = true;
  // }
  
  //The followong code needs to be added to html
  //<app-alert *ngIf="isAlert" [type]="''" (onClose)="onAlertClose()"
  // [messageHeader] ="''" [messageContent]="''" ></app-alert>
}
