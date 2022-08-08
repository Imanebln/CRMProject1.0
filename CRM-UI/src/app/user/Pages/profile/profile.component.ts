import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Address } from '../../Models/Address.models';
import { ContactDetails } from '../../Models/ContactDetails.models';
import { ContactService } from '../../Services/contact.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  contact : ContactDetails;
  colors : string[] = ['#3A86FF', '#8B45ED', '#FFBE0B'];
  AddressShowed : boolean = false;
  selectedAddress : Address = <Address>{};
  countries : any[];
 
  constructor(private contactService : ContactService, private router : Router) {
    this.contactService.getCurrentUser().subscribe(user => {
      this.contact = user as ContactDetails;
      this.contact.addresses = this.contact.addresses.reverse()
    })
    this.contactService.getCountries().subscribe(countries => {
      this.countries = countries.sort((a,b) => a.name > b.name?1:-1)
    })
   }
  
  ngOnInit(): void {
  }

  editContact = () => {
    this.router.navigate(['/user/contacts'])
  }
  showAddress = () => {
    let i = 0;
    if (!this.AddressShowed){
      document.querySelectorAll(".address").forEach((elm : any) => {
        setTimeout(()=> {
          elm.style.display = 'block'
          elm.style.animation = 'ShowAddress 300ms ease-out';
        }, i*200)
        i++;
      })
    } else {
      document.querySelectorAll(".address").forEach((elm : any) => {
        setTimeout(()=> {
          elm.style.animation = 'HideAddress 300ms ease-out';
          setTimeout(()=> {
            elm.style.display = 'none'
          },300)
        }, i*200)
        i++;
      })
    }
    this.AddressShowed = !this.AddressShowed
  }

  UpdateAddress = (address : Address) => {
    this.contactService.updateAddress(address).subscribe({
      next: (response) => console.log(response),
      error: (response) => console.log(response)
    })
  }

}
