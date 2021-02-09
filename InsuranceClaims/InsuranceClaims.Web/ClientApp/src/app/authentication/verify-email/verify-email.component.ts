import { Component, Injector, OnInit } from '@angular/core';
import { takeUntil } from 'rxjs/operators';
import { AccountController } from 'src/@core/APIs/AccountController';
import { BaseService } from 'src/@core/services/base.service';

@Component({
  selector: 'app-verify-email',
  templateUrl: './verify-email.component.html',
  styleUrls: ['./verify-email.component.scss']
})
export class VerifyEmailComponent extends BaseService implements OnInit {

  email: string;
  token: string;
  verifed = false;

  constructor(public injector: Injector) {
    super(injector);

    this.activatedRoute.queryParams
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(params => {
        if (params && params['token'] && params['email']) {
          this.token = params['token'];
          this.email = params['email'];
          this.confirmEmail();
        }

      });

  }

  ngOnInit() {
  }

  confirmEmail() {


    const authData = {
      email: this.email,
      token: this.token,
    };

    this.httpService.POST(AccountController.Login, authData)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        if (res.isPassed) {
          this.alertService.success(`${this.email} has been verified successful. Please log in to your account`);
          this.verifed = true;
        } else {
          this.alertService.error(res.message);
          this.verifed = false;
          this._ref.detectChanges();
        }
      }, err => {
        this.alertService.exception();
        this.verifed = false;
        this._ref.detectChanges();
      });

  }

}
