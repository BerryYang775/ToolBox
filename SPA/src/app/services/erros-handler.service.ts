import { Injectable } from '@angular/core';
import { APIOperationError, DataValidationError } from '../models/problem-detail';
import { ToastService } from 'ng-devui';

@Injectable({
  providedIn: 'root'
})
export class ErrosHandlerService {

  constructor(private toastService: ToastService) { }

  handleAPIErr(e: any) {
    if (e.status == 500) {
      // API Operation Error
      let err = e.error as APIOperationError
      this.toastService.open({
        value: [{ severity: 'error', summary: 'Error Occurred', content: err.detail }]
      })
    } else if (e.status == 400) {
      let err = e.error as DataValidationError;
      var reasons = err.reasons.join("<br/>");
      this.toastService.open({
        value: [{ severity: 'error', summary: 'Data Validation Error', content: reasons }]
      })
    }
  }

  // showAlert(message: string, title: string){
  //   // alert(message, title);
  // }
}
