import { Component, OnInit, Injector } from '@angular/core';
import { BaseService } from 'src/@core/services/base.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AccountController } from 'src/@core/APIs/AccountController';
import { takeUntil } from 'rxjs/operators';
import { RxwebValidators } from '@rxweb/reactive-form-validators';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent extends BaseService implements OnInit {

  form: FormGroup;
  // params
  email: string;
  token: string;

  constructor(
    public injector: Injector,
    private fb: FormBuilder,
  ) {
    super(injector);
    this.activatedRoute.queryParams.subscribe(params => {
      this.email = params['email'];
      this.token = params['token'];
    });
  }

  ngOnInit() {
    this.form = this.fb.group({
      newPassword: [null, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(320)])],
      newPassword2: [null, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(320), RxwebValidators.compare({fieldName:'newPassword'})])],
    });
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

    if (controls.newPassword.value != controls.newPassword2.value) {
      this.alertService.error('Password not matched');
      return;
    }

    const resetPasswordData = {
      token: this.token,
      email: this.email,
      newPassword: controls.newPassword.value,
    };

    this.httpService.POST(AccountController.ResetPassword, resetPasswordData)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        if (res.isPassed) {
          this.alertService.success('Your password has been reset successfully');
          this.form.reset();
          this.router.navigate(['/auth/login']);
        } else {
          this.alertService.error(res.message);
          this._ref.detectChanges();
        }
      }, err => {
        this.alertService.exception();
        this._ref.detectChanges();
      });
  }

}
