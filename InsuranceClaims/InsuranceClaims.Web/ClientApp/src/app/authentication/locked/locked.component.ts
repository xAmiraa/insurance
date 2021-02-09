import { Component, OnInit, Injector } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseService } from 'src/@core/services/base.service';
import { AccountController } from 'src/@core/APIs/AccountController';
import { takeUntil } from 'rxjs/operators';
declare const $: any;

@Component({
  selector: 'app-locked',
  templateUrl: './locked.component.html',
  styleUrls: ['./locked.component.scss']
})
export class LockedComponent extends BaseService implements OnInit {

  form: FormGroup;
  submitted = false;
  returnUrl: string;
  hide = true;

  constructor(
    private formBuilder: FormBuilder,
    public injector: Injector
  ) {
    super(injector);
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      password: [null, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(320)])],
    });
    // get return url from route parameters or default to '/'
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';
    //    [Focus input] * /
    $('.input100').each(function () {
      $(this).on('blur', function () {
        if (
          $(this)
            .val()
            .trim() != ''
        ) {
          $(this).addClass('has-val');
        } else {
          $(this).removeClass('has-val');
        }
      });
    });
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

    const authData = {
      email: this.loggedInUser.email,
      password: controls.password.value,
    };

    this.httpService.POST(AccountController.Login, authData)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        if (res.isPassed) {
          this.authService.updateToken(res.data);
          this.authService.updateTokenExpirationStatus(false);
          this.alertService.success('You have logged in successfully');
          this.router.navigate([`/dashboard`]);
        } else {

        }
      }, err => {
        this.alertService.exception();
        this._ref.detectChanges();
      });
  }
}
