import { Component, OnInit,Input } from '@angular/core';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css']
})
export class ToastComponent implements OnInit {

    @Input() toastModel : ToastModel;
  constructor() { }

  ngOnInit(): void {
    console.log("First line of show toast");
    this.toast.content = this.toastModel.content;
    if (this.toastModel.icon != null)
      {
        this.toast.icon = this.toastModel.icon; 
        console.log(this.toast.icon);
      }

    if(this.toastModel.type != null)
     { 
       this.toast.type = this.toastModel.type;          
      }
    this.style.animation = 'ShowUpAnimation 600ms ease-in';
    this.style.display = 'grid';
    console.log(this.style);
    
  }
  closeToast = () => {
   this.style.animation = 'CollapseAnimation 600ms ease';
    setTimeout(() => {
      this.style.display = 'none' ;
    }, 600);
    
  }
  toast : ToastModel =<ToastModel>{
    icon : 'cirlce-check',
    type : 'success',
  }

  style : any = {
    display :  'none',
    border : '1px  ',

  }
  



 

}
export interface ToastModel{
 icon?: string;
 type?: string;
 content : string; 
}


