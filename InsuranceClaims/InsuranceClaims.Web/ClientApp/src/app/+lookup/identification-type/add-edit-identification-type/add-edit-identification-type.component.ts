import { Component, OnInit, Inject, Injector } from '@angular/core';
import { BaseService } from 'src/@core/services/base.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IdentificationTypesController } from 'src/@core/APIs/IdentificationTypesController';
import { takeUntil, map } from 'rxjs/operators';
import { IdentificationTypeFilterDto } from 'src/@core/models/lookup/IdentificationType';


@Component({
  selector: 'app-add-edit-identification-type',
  templateUrl: './add-edit-identification-type.component.html',
  styleUrls: ['./add-edit-identification-type.component.scss']
})
export class AddEditIdentificationTypeComponent extends BaseService implements OnInit {
  form: FormGroup;
  mode: 'create' | 'update' = 'create';
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  public dialogRef: MatDialogRef<AddEditIdentificationTypeComponent>,
  private fb: FormBuilder,
    public injector: Injector) {
      super(injector);
   }

  ngOnInit(): void {
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

    let body: IdentificationTypeFilterDto = this.form.value;

    this.httpService.POST(IdentificationTypesController.CreateIdentificationType, body)
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

    let body: IdentificationTypeFilterDto = this.form.value;

    this.httpService.PUT(IdentificationTypesController.UpdateIdentificationType(this.data?.id), body)
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
