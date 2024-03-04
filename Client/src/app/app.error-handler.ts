import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { IndividualConfig, ToastrService } from 'ngx-toastr';
import * as Raven from 'raven-js';

@Injectable()
export class AppErrorHandler implements ErrorHandler {
  private toastr!: ToastrService;

  constructor(private injector: Injector) {}

  handleError(error: any): void {
    Raven.captureException(error.originalError || error);

    console.log(error);
    // Lazy-load ToastrService using Angular's Injector
    if (!this.toastr) {
      this.toastr = this.injector.get(ToastrService);
    }

    const config: Partial<IndividualConfig> = {
      closeButton: true,
      timeOut: 5000,
      extendedTimeOut: 2000,
      tapToDismiss: false,
      positionClass: 'toast-top-right',
      titleClass: 'toast-title',
      messageClass: 'toast-message',
    };
    this.toastr.error('Something went wrong!', 'Error', config);
  }
}
