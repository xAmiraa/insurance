import { Component, OnInit, Inject, Injector } from '@angular/core';
import { BaseService } from 'src/@core/services/base.service';
import { CreateUpdatePolicyInsurerDto } from 'src/@core/models/lookup/PolicyInsurer';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PolicyInsurersController } from 'src/@core/APIs/PolicyInsurersController';
import { takeUntil, map } from 'rxjs/operators';

@Component({
  selector: 'app-add-edit-policy-insurer',
  templateUrl: './add-edit-policy-insurer.component.html',
  styleUrls: ['./add-edit-policy-insurer.component.scss']
})
export class AddEditPolicyInsurerComponent extends BaseService implements OnInit {

  form: FormGroup;
  mode: 'create' | 'update' = 'create';

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<AddEditPolicyInsurerComponent>,
    private fb: FormBuilder,
    public injector: Injector) {
    super(injector);
  }

  ngOnInit() {

    this.form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      description: new FormControl(null),
    });

    if (this.data && this.data.id) {
      this.mode = 'update';
      this.setFormData();
    }

  }

  setFormData() {
    this.form.controls['name'].patchValue(this.data?.name);
    this.form.controls['description'].patchValue(this.data?.description);
    this._ref.detectChanges();
  }

  save() {
    const controls = this.form.controls;
    /** check form */
    if (this.form.invalid) {
      Object.keys(controls).forEach(controlName =>
        controls[controlName].markAsTouched()
      );
      return;
    }

    if (this.mode === 'create') {
      this.createObject();
    } else if (this.mode === 'update') {
      this.updateObject();
    }

  }


  createObject() {
    this.showSpinner();

    let body: CreateUpdatePolicyInsurerDto = this.form.value;

    this.httpService.POST(PolicyInsurersController.CreatePolicyInsurer, body)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        if (res.isPassed) {
          this.spinner.hide();
          this.alertService.success(res.message);
          this.dialogRef.close('done');
        } else {
          this.errors = res.errors;
          this.spinner.hide();
        }
      });
  }


  updateObject() {
    this.showSpinner();

    let body: CreateUpdatePolicyInsurerDto = this.form.value;

    this.httpService.PUT(PolicyInsurersController.UpdatePolicyInsurer(this.data?.id), body)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        if (res.isPassed) {
          this.spinner.hide();
          this.alertService.success(res.message);
          this.dialogRef.close('done');
        } else {
          this.errors = res.errors;
          this.spinner.hide();
        }
      });
  }


  isCreateMode() {
    return this.mode === 'create';
  }

  isUpdateMode() {
    return this.mode === 'update';
  }

}
