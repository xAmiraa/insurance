import { Injectable } from '@angular/core';
import swal, { SweetAlertOptions } from 'sweetalert2';
import { HttpService } from './http.service';

@Injectable()
export class SweetAlertService {

  constructor(public httpService: HttpService) { }

  // Ajax Request
  Action(url: string, options?: SweetAlertOptions) {
    return swal.fire({
      title: 'Are you sure?',
      text: options?.text || `You won't be able to revert this!`,
      buttonsStyling: false,
      showCancelButton: true,
      confirmButtonText: options?.confirmButtonText || 'Yes, cancel it!',
      cancelButtonText: 'Close',
      showLoaderOnConfirm: true,
      icon: 'warning',
      customClass: {
        confirmButton: options?.customClass?.confirmButton || 'btn btn-danger',
        cancelButton: 'btn btn-default ml-1'
      },
      preConfirm: (inputValue) => {

        return this.httpService.POST(url)
          .toPromise()
          .then(response => {
            if(response.isPassed) {
              return response;
            }
            else {
              response.errors?.forEach(err => swal.showValidationMessage(err));
            }
          })
          .catch(error => {
            swal.showValidationMessage(
              `Request failed: ${error}`
            )
          });
      },
      allowOutsideClick: () => !swal.isLoading()
    });
  }

  Delete(url: string, options?: SweetAlertOptions) {
    return swal.fire({
      title: 'Are you sure?',
      text: options?.text || `You won't be able to revert this!`,
      buttonsStyling: false,
      showCancelButton: true,
      confirmButtonText: options?.confirmButtonText || 'Yes, delete it!',
      cancelButtonText: 'Close',
      showLoaderOnConfirm: true,
      icon: 'warning',
      customClass: {
        confirmButton: options?.customClass?.confirmButton || 'btn btn-danger',
        cancelButton: 'btn btn-default ml-1'
      },
      preConfirm: (inputValue) => {

        return this.httpService.DELETE(url)
          .toPromise()
          .then(response => {
            if(response.isPassed) {
              return response;
            }
            else {
              response.errors?.forEach(err => swal.showValidationMessage(err));
            }
          })
          .catch(error => {
            swal.showValidationMessage(
              `Request failed: ${error}`
            )
          });
      },
      allowOutsideClick: () => !swal.isLoading()
    });
  }

  ActionWithInput(url: string, options?: SweetAlertOptions, inputIsRequired: boolean = true) {
    return swal.fire({
      title: options?.title?.toString() || 'Confirm',
      text: options?.text ? options?.text : null,
      buttonsStyling: false,
      showCancelButton: true,
      confirmButtonText: options?.confirmButtonText || 'Confirm',
      cancelButtonText: 'Close',
      showLoaderOnConfirm: true,
      icon: 'warning',
      customClass: {
        confirmButton: options?.customClass?.confirmButton || 'btn btn-danger',
        cancelButton: 'btn btn-default ml-1'
      },
      // input props
      input: 'textarea',
      inputPlaceholder: options?.inputPlaceholder || 'type something',
      inputValidator: (value) => {
        if (!value && inputIsRequired) {
          return 'You need to write something!'
        }
      },
      preConfirm: (inputValue) => {

        const body = {
          comments: inputValue
        }

        return this.httpService.POST(url, body)
          .toPromise()
          .then(response => {
            if(response.isPassed) {
              return response;
            }
            else {
              response.errors?.forEach(err => swal.showValidationMessage(err));
            }
          })
          .catch(error => {
            swal.showValidationMessage(
              `Request failed: ${error}`
            )
          });
      },
      allowOutsideClick: () => !swal.isLoading()
    });
  }
}
