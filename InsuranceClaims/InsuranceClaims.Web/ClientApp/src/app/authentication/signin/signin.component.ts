import { Component, OnInit, Injector } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseService } from '../../../@core/services/base.service';
import { AccountController } from '../../../@core/APIs/AccountController';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent extends BaseService implements OnInit {

  form: FormGroup;
  returnUrl: string;

  constructor(
    private formBuilder: FormBuilder,
    public injector: Injector
  ) {
    super(injector);
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      email: [null, Validators.compose([Validators.required, Validators.email, Validators.minLength(3), Validators.maxLength(320)])],
      password: [null, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(320)])],
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';
  }

  get f() {
    return this.form.controls;
  }

  onSubmit() {
    const controls = this.form.controls;
    /** check form */
    if (this.form.invalid) {
      Object.keys(controls).forEach(controlName =>
        controls[controlName].markAsTouched()
      );
      return;
    }

    this.showSpinner();

    const body = this.form.value;

    this.httpService.POST(AccountController.Login, body)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        if (res.isPassed) {
          const token = res.data;
          this.authService.updateToken(token);
          this.loadUserInfo();
        } else {
          this.errors = res.errors;
          this.spinner.hide();
        }
      });
  }

  loadUserInfo() {
    this.httpService.GET(AccountController.GetLoggedinUserInfo)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        if (res.isPassed) {
          this.authService.updateStoredUserInfo(res.data);
          this.spinner.hide();
          this.errors = [];
          this.router.navigate(['/']);
        } else {
          this.errors = res.errors;
          this.spinner.hide();
        }
      });
  }
}
