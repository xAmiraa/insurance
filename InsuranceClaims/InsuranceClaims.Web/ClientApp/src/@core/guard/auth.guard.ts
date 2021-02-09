
import { Injectable } from '@angular/core';
import {
  CanActivate, Router, ActivatedRouteSnapshot,
  RouterStateSnapshot, CanLoad, Route
} from '@angular/router';
import { AuthService } from '../services/auth.service';


@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate, CanLoad {
  canLoad(route: Route): boolean {

    const user = JSON.parse(localStorage.getItem('claim_user'));
    if (user && user.token) {

      if (user.is_token_expired) {
        this.router.navigate(['/auth/locked']);
        return false;

      } else {
        return true;
      }

    } else {
      this.router.navigate(['/auth/login']);
      return false;
    }
  }

  constructor(private router: Router, private authService: AuthService) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    const user = JSON.parse(localStorage.getItem('claim_user'));
    if (user && user.token) {

      if (user.is_token_expired) {
        this.router.navigate(['/auth/locked']);
        return false;

      } else {
        return true;
      }

    } else {
      this.router.navigate(['/auth/login']);
      return false;
    }

  }

}
