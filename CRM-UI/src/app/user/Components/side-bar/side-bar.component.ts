import { Component, OnInit } from '@angular/core';
import Items from './sidebar.json';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {
  navItems : any[] = Items.items;
  active : string;
  constructor() { }

  ngOnInit(): void {
    this.active = window.location.pathname.split('/')[2];
  }

  activeLink(link : string){
    this.active = link;
  }

}
