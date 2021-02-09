import { Component } from '@angular/core';
import {
  Event,
  Router,
  NavigationStart,
  NavigationEnd,
} from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { PlatformLocation } from '@angular/common';
import { NgxPermissionsService } from 'ngx-permissions';
import { getStoredData } from 'src/@core/models/security/User';
import { ApplicationRolesEnum } from 'src/@core/models/enum/Enums';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  currentUrl: string;

  constructor(public _router: Router, location: PlatformLocation, private spinner: NgxSpinnerService, private permissionsService: NgxPermissionsService) {
    this._router.events.subscribe((routerEvent: Event) => {
      if (routerEvent instanceof NavigationStart) {
        this.spinner.show();
        location.onPopState(() => {
          window.location.reload();
        });
        this.currentUrl = routerEvent.url.substring(
          routerEvent.url.lastIndexOf('/') + 1
        );
      }
      if (routerEvent instanceof NavigationEnd) {
        this.spinner.hide();
      }
      window.scrollTo(0, 0);
    });
  }

  ngOnInit(): void {
    this.permissionsService.permissions$.subscribe(res => {
      console.log(res);
    })
    this.loadPermissions();
  }

  loadPermissions() {
    const user = getStoredData()?.user;
    let permissions: string[] = [];

    if (ApplicationRolesEnum[user?.role?.id]) {
      permissions.push(ApplicationRolesEnum[user?.role?.id]);
    }
    if (user?.groupName?.replace(/\s/g, '')) {
      permissions.push();
    }

    this.permissionsService.loadPermissions(permissions);
  }


}
