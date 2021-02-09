import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { getStoredData, LoggedInUser } from '../models/security/User';
import { ApplicationRolesEnum } from '../models/enum/Enums';
import { NgxPermissionsService } from 'ngx-permissions';

@Injectable()
export class AuthService {
  // logged in user
  public loggedInUser$: BehaviorSubject<LoggedInUser> = new BehaviorSubject<LoggedInUser>(null);

  // loadingAction
  public loadingAction$: BehaviorSubject<Boolean> = new BehaviorSubject<Boolean>(false);

  constructor(private router: Router, private permissionsService: NgxPermissionsService) {
    this.loggedInUser$.next(getStoredData()?.user);
    this.loadingAction$.next(false);
  }


  // store user data after login succeffully
  updateStoredUserInfo(user: LoggedInUser) {
    let storedData = JSON.parse(localStorage.getItem('claim_user')) || {};
    storedData.user = user
    localStorage.setItem('claim_user', JSON.stringify(storedData));

    this.loggedInUser$.next(storedData.user);

    this.loadPermissions();
  }

  loadPermissions() {
    const user = getStoredData()?.user;
    let permissions: string[] = [];
    permissions.push(ApplicationRolesEnum[user.role?.id]);
    if (user?.groupName?.replace(/\s/g, '')) {
      permissions.push();
    }
    this.permissionsService.loadPermissions(permissions);
  }

  updateToken(token: string) {
    let storedData = JSON.parse(localStorage.getItem('claim_user')) || {};
    storedData.token = 'Bearer ' + token;
    localStorage.setItem('claim_user', JSON.stringify(storedData));
  }

  updateTokenExpirationStatus(isExpired: boolean) {
    let storedData = JSON.parse(localStorage.getItem('claim_user')) || {};
    storedData.is_token_expired = isExpired;
    localStorage.setItem('claim_user', JSON.stringify(storedData));
  }

  // logout
  logout() {
    // localStorage.clear();
    localStorage.removeItem('claim_user');
    this.router.navigate(['/auth/login']);
  }

  // loading action
  ActionLoading(val: boolean) {
    this.loadingAction$.next(Boolean(val));
  }

}
