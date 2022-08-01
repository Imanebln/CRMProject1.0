import { Component, EventEmitter, Host, HostBinding, Input, OnInit, Output, AfterViewInit, HostListener } from '@angular/core';
import Items from './sidebar.json';
import { Popover } from "bootstrap";

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit, AfterViewInit {
  PopoverList : any[];
  navbarSmall :string = '75px';
  navbarWide : string = 'max(15vw, 300px)'
  @Input() LeftSpace:string;
  @Output() LeftSpaceChange = new EventEmitter();
  navItems : any[] = Items.items;
  active : string;
  minimized : boolean = false;


  constructor() { }
  ngAfterViewInit(): void {
    this.initPopovers()
  }

  ngOnInit(): void {
    this.active = window.location.pathname.split('/')[2];
    this.width = this.LeftSpace
    document.addEventListener('click', (event :any)=>{
        let sidebar = document.querySelector('#sidebar')
        let items = document.querySelectorAll('.items > *');
        if(event.target.contains(sidebar)){
          sidebar?.classList.remove('sidebar-show');
        }
    })
  }
  

  @HostBinding('style.width') width:string;

  @HostListener('window:resize', ['$event'])
    onResize(event:any) {
      if(event.target.innerWidth <= 768){
        this.minimized = true;
        this.minimize(document.querySelector('.next'))
      }
      
  }

  activeLink(link : string){
    this.active = link;
  }

  minimize(nextBtn : any){
    if (!this.minimized){
      this.width = this.navbarSmall;
      this.LeftSpaceChange.emit(this.navbarSmall)
      document.querySelectorAll('.toggleable').forEach(e =>{
          e.classList.add('hidden')
      })
      nextBtn.classList.add('rotate')  
      this.PopoverList.forEach(e => e.enable())
    }else{
      this.width = this.navbarWide;
      this.LeftSpaceChange.emit(this.navbarWide)
      this.PopoverList.forEach(e => e.disable())
      document.querySelectorAll('.toggleable').forEach(e =>{
        e.classList.add('showing-on')
      })
      setTimeout(()=> {
        document.querySelectorAll('.toggleable').forEach(e =>{
          e.classList.remove('hidden')
          e.classList.remove('showing-on')
        })
      }, 800)
      nextBtn.classList.remove('rotate')
    }
    this.minimized = !this.minimized;
  }


  initPopovers(){
    this.PopoverList = [].slice.call(
      document.querySelectorAll('.item')
    ).map(function (el: Element) {
      let item = Popover.getOrCreateInstance(el);
      item.disable();
      return item;
    });
  }

}
