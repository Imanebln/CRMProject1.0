import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { ContactService } from './contact.service';

@Injectable({
  providedIn: 'root',
})
export class ContactGuard implements CanActivate {
  isPrimary: boolean = false;
  constructor(private contactService: ContactService, private router: Router) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    this.contactService.getCurrentUser().subscribe((value) => {
      this.isPrimary = value.isPrimary;
    });
    if (this.isPrimary == true) {
      return true;
    }
    this.router.navigate(['user']);
    return false;
  }
}
