import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if(error){
          if(error.status == 400){
            if(error.error.errors){
              throw error.error;
            } else {
              // error is the response so error.message will be the generic message:
              // "Http failure response for https://localhost:5001/api/buggy/badrequest: 400 Bad Request"
              // Within the response there is an error property and a message prop within that with our custom
              // message. So we use error.error.message to get our message
              this.toastr.error(error.error.message, error.status.toString())
            }
            
          };
          if(error.status == 401){

          }
          if(error.status == 404){
            this.router.navigateByUrl('/not-found');
          };
          if(error.status == 500){
            const navigationExtras: NavigationExtras = {state: {error: error.error}};
            this.router.navigateByUrl('/server-error', navigationExtras);
          };
        };
        return throwError(() => new Error(error.message));
      })
    )
  }
}
