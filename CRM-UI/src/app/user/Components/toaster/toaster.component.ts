import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastComponent, ToastModel } from '../toast/toast.component';

@Component({
  selector: 'app-toaster',
  templateUrl: './toaster.component.html',
  styleUrls: ['./toaster.component.css']
})
export class ToasterComponent implements OnInit {

  @ViewChild(ToastComponent) Toast : ToastComponent;

  currentToasts: ToastModel[] = [];


  constructor() { }

  ngOnInit(): void {
  }

  addToast(toast : ToastModel){
    this.currentToasts.push(toast);    
  }
}
