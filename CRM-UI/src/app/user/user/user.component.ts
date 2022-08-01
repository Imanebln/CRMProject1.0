import { Component, HostListener, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  SpaceLeft: string = 'max(15vw, 300px)';
  constructor() {}

  ngOnInit(): void {}

  showSidebar(event: any, sidebar: any) {
    sidebar.classList.add('sidebar-show');
  }
}
