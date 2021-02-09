import { OnDestroy, Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Injector, ChangeDetectorRef } from "@angular/core";
import { Router, ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
// Services
import { HttpService } from './http.service';
import { AuthService } from './auth.service';
// Lib
import * as FileSaver from 'file-saver';
import * as numeral from 'numeral';
import * as moment from 'moment';
import { getStoredData, LoggedInUser } from '../models/security/User';
import { AbstractControl } from '@angular/forms';
import { NgxSpinnerService } from "ngx-spinner";
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { SweetAlertService } from './sweet-alerts.service';
import { EnumService } from './enum.service';
import { NgxPermissionsService } from 'ngx-permissions';
import { KeyValueObject, QueryParamsDto } from '../models/common/SharedModels';
import { AlertService } from './alert.service';
import { MatDialog } from '@angular/material/dialog';
export class ServiceLocator {
  static injector: Injector;
}

@Injectable()
export class BaseService implements OnDestroy {

  loggedInUser: LoggedInUser;
  modalReference: any;
  errors: string[] = [];
  ngUnsubscribe = new Subject<void>();

  //#region Services
  public alertService: AlertService;
  public datepipe: DatePipe;
  public authService: AuthService;
  public httpService: HttpService;
  public router: Router;
  public activatedRoute: ActivatedRoute;
  public _ref: ChangeDetectorRef;
  public spinner: NgxSpinnerService;
  public modalService: NgbModal;
  public dialog: MatDialog;
  public sweetAlertService: SweetAlertService;
  public enumService: EnumService;
  public permissionsService: NgxPermissionsService;
  //#endregion


  constructor(public injector: Injector) {
    this.alertService = this.injector.get(AlertService);
    this.datepipe = this.injector.get(DatePipe);
    this.authService = this.injector.get(AuthService);
    this.httpService = this.injector.get(HttpService);
    this.router = this.injector.get(Router);
    this.activatedRoute = this.injector.get(ActivatedRoute);
    this.spinner = this.injector.get(NgxSpinnerService);
    this.modalService = this.injector.get(NgbModal);
    this.sweetAlertService = this.injector.get(SweetAlertService);
    this.enumService = this.injector.get(EnumService);
    this._ref = this.injector.get(ChangeDetectorRef);
    this.permissionsService = this.injector.get(NgxPermissionsService);
    this.dialog = this.injector.get(MatDialog);

    this.loggedInUser = getStoredData()?.user;
  }

  detectChanges() {
    // Programmatically run change detection to fix issue in Safari
    setTimeout(() => {
      this._ref.detectChanges();
    }, 500);
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  //#region Validation Methods
  isEmpty(str) {
    return (!str || 0 === str.length);
  }

  isBlank(str) {
    return (!str || /^\s*$/.test(str));
  }
  //#endregion


  //#region Formatting Methods
  roundNumber(num: number): number {
    if (num) {
      return Number(num.toFixed(2));
    }
    return 0;
  }

  formatInt(v) {
    if (v) {
      return numeral(v).format('0,0');
    } else {
      return 0;
    }
  }

  formatFloat(v) {
    if (v) {
      return numeral(v).format('0,0.00')
    } else {
      return 0;
    }
  }

  formatPrice(p): string {
    if (p) {
      return numeral(p).format(`$0,0.00`);
    } else {
      return '0';
    }
  }

  formatDate(date: string) {
    if (!moment(new Date(date), 'd MMM y').isValid() || !date) {
      return '-'
    }
    return moment(new Date(date)).format('D MMM YYYY');
  }


  formatMonthYear(date: string) {
    if (date) {
      return this.datepipe.transform(new Date(date), 'MMM y');
    } else {
      return '-';
    }
  }

  formatMonthDay(date: string) {
    if (date) {
      return moment(date).format('D MMMM');
    } else {
      return '-';
    }
  }

  formatDateTime(date: string) {
    if (!moment(new Date(date), 'd MMM y').isValid() || !date) {
      return '-'
    }
    return moment(date).format('D MMM YYYY, hh:mm a');
  }


  getReadableFileSize(sizeInBytes: number) {
    var _size = sizeInBytes;
    var fSExt = new Array('Bytes', 'KB', 'MB', 'GB'),
      i = 0;
    while (_size > 900) { _size /= 1024; i++; }
    var exactSize = (Math.round(_size * 100) / 100) + ' ' + fSExt[i];
    return exactSize;
  }

  extractContent(s) {
    var span = document.createElement('span');
    span.innerHTML = s;
    return span.textContent || span.innerText;
  }

  convertTime24To12(time): string {
    return moment(time, "HH:mm").locale('en').format("h:mm A");
  }

  convertTime12To24(time: string): string {
    return moment(time, "h:mm A").locale('en').format("HH:mm");
  }
  //#endregion


  //#region Bootstrap Modals Methods
  open(content, options = {}) {
    this.modalReference = this.modalService.open(content, options);
    this.modalReference.result.then((result) => {
      console.log(`Closed with: ${result}`);
    }, (reason) => {
      console.log(`Dismissed ${this.getDismissReason(reason)}`);
    });
  }

  public getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
  //#endregion


  groupBy(array: any[], key: string) {

    return array.reduce((objectsByKeyValue, obj) => {
      const value = obj[key];
      objectsByKeyValue[value] = (objectsByKeyValue[value] || []).concat(obj);
      return objectsByKeyValue;
    }, {});

    /***
     * to loop in object
     * *ngFor="let record of defaultRoleActions | keyvalue"
     * {{ record.key }}
     * {{ record.value }}
     */

  }

  convertEnumToList(data: any): KeyValueObject[] {
    return Object.keys(data).map(key => ({ id: data[key], name: key }))
  }

  // get nested property from the object to display it on the ngx-datatable
  getRowData(row: object, prop: string) {
    let props = prop.split('.');

    if (!props || props?.length == 0 || !row) return null;
    else if (props?.length == 1) return row[props[0]];
    else if (props?.length == 2 && row[props[0]]) return row[props[0]][props[1]];
    else if (props?.length == 3 && row[props[0]][props[1]]) return row[props[0]][props[1]][props[2]];
    else if (props?.length == 4 && row[props[0]][props[1]][props[2]]) return row[props[0]][props[1]][props[2]][props[3]];
    else if (props?.length == 5 && row[props[0]][props[1]][props[2]][props[3]]) return row[props[0]][props[1]][props[2]][props[3]][props[4]];
    return null;
  }

  showSpinner(options = { fullScreen: false }) {
    this.spinner.show(undefined,
      {
        type: 'ball-triangle-path',
        size: 'medium',
        bdColor: 'rgba(0, 0, 0, 0.8)',
        color: '#fff',
        fullScreen: options.fullScreen,
      });
  }

  isControlHasError(form: any, controlName: string, validationType: string): boolean {
    const control = form.controls[controlName];
    if (!control) {
      return false;
    }

    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }

  isControlRequired(form: any, controlName: string): boolean {
    const form_field = form.get(controlName);
    if (!form_field.validator) {
      return false;
    }

    const validator = form_field.validator({} as AbstractControl);
    if (validator && validator.required) {
      return true;
    }
    return false;
  }

  isValidControl(form: any, controlName: string): boolean {
    const control = form.controls[controlName];
    if (!control) {
      return false;
    }

    if (control.valid && (control.dirty || control.touched)) {
      return true;
    }

    return false;
  }

  isInvalidControl(form: any, controlName: string): boolean {
    const control = form.controls[controlName];
    if (!control) {
      return false;
    }

    if (control.invalid && (control.dirty || control.touched)) {
      return true;
    }

    return false;
  }

  numericOnly(event): boolean {
    let patt = /^([0-9])$/;
    let result = patt.test(event.key);
    return result;
  }


  getDateFromPicker(date: { day: number, month: number, year: number }) {
    if (!date) return null;

    const zeroPad = (num, places) => String(num).padStart(places, '0');
    return `${date.year}-${zeroPad(date.month, 2)}-${zeroPad(date.day, 2)}T10:00`;
  }
  appendTimeToDateInput(date: string) {
    if (!date) return null;
    return `${date}T10:00`;
  }

  setDateInput(date: string) {
    if (!date) return;
    return new Date(date).toISOString().split('T')[0];
  }
  patchDatePicker(date: string): NgbDate {
    if (!date) return null;
    let convertedDate = moment(date);
    return new NgbDate(convertedDate.year(), convertedDate.month() + 1, convertedDate.date())
  }

  public downloadFile(url: string): void {
    var name = url.substr(url.lastIndexOf('\\') + 1);
    FileSaver.saveAs(url, name);
  }

  getFilterParamsDtos(filterDto: any, skippedParams: string[] = []): QueryParamsDto[] {
    let skipped: string[] = ['pageSize', 'pageIndex', 'applySort', 'sortProperty', 'isAscending'].concat(skippedParams);
    let result: QueryParamsDto[] = [];

    if (filterDto != null) {
      Object.keys(filterDto).forEach(key => {
        if (!skipped.includes(key) && filterDto[key] != null && filterDto[key] != undefined) {
          if (Array.isArray(filterDto[key])) { // Incase you pass array of Ids 
            let arr = filterDto[key] as string[];
            result.push({ key: key, value: arr.join(',') });
          } else if (typeof filterDto[key] == 'object') {
            result.push({ key: key, value: new Date(filterDto[key]).toISOString() });
          } else {
            result.push({ key: key, value: filterDto[key] });
          }
        }
      });
    }

    return result;
  }
}

