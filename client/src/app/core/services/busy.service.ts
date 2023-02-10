import { Injectable } from '@angular/core';
import { NgxSpinner, NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0

  constructor(private spinnerService: NgxSpinnerService) { }

  busy(){
    this.busyRequestCount++;
    // undefined here but takes name of spinner -- we did not name our spinner -- next param are options
    this.spinnerService.show(undefined, {
      type: 'line-scale',
      bdColor: 'rgba(255,255,255,0.7)',
      color: '#333333'
    })
  }


  idle(){
    this.busyRequestCount--;
    if(this.busyRequestCount <= 0){
      this.busyRequestCount = 0;
      this.spinnerService.hide();
    }
  }



}
