import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { lastValueFrom } from "rxjs";
import { Address } from '../../Models/Address.models';
import { ContactDetails } from '../../Models/ContactDetails.models';
import { ContactService } from '../../Services/contact.service';
import compress from 'compress-base64';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  contact : ContactDetails | any;
  colors : string[] = ['#3A86FF', '#8B45ED', '#FFBE0B'];
  AddressShowed : boolean = false;
  selectedAddress : Address = <Address>{};
  countries : any[];
  public ImageUrl : string; 
 
  constructor(private contactService : ContactService, private router : Router) {
   }

  @ViewChild('avatar')
  avatar : ElementRef

  @ViewChild('saveAvatar')
  saveAvatar : ElementRef

  ngOnInit() {
    this.initUser()
    this.contactService.getCountries().subscribe(countries => {
      this.countries = countries.sort((a,b) => a.name > b.name?1:-1)
    })
  }


  
  editContact = () => {
    this.router.navigate(['/user/contacts'])
  }

  onFileSelected = (event:any) => {
    let imageReader : FileReader = new FileReader();
    imageReader.readAsDataURL(event.target.files[0]);
    let url : string = imageReader.result as string;
    imageReader.onload = () => {
      console.log(imageReader.result);
      
      compress(imageReader.result as string, {
        width: 128,
        quality: 0.8,
        max:200,
        min:10
      }).then((result : any) =>{
        console.log(result);
        
        this.avatar.nativeElement.src = result;
        url = result as string;
        this.contact.imageUrl = url.split(",")[1];
        this.saveAvatar.nativeElement.classList.toggle('d-none')
      })
      
      
      
    }
  }

  onImageSave = () => {
    this.saveAvatar.nativeElement.classList.toggle('d-none')
    this.contactService.updateContact(this.contact).subscribe((res)=>{
      console.log(res)
    })
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

  UpdateContact(contact : ContactDetails){
    this.contactService.updateContact(contact).subscribe({
      next: (response) => console.log(response),
      error: (response) => console.log(response)
    })
  }

  refreshUser(){
    this.initUser().then(res => console.log(res))
  }

  initUser = async ()=>{
    let response : ContactDetails = await lastValueFrom(this.contactService.getCurrentUser()) as ContactDetails;
    this.contact = response;
    console.log(this.contact);
    
    this.ImageUrl = 'data:image/png;base64,' + this.contact.imageUrl;
    this.contact.addresses = this.contact.addresses.reverse()
  }
}
