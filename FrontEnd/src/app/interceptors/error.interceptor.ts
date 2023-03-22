import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
  } from '@angular/common/http';
  import { Injectable } from '@angular/core';
  import { ToastrService } from 'ngx-toastr';
  import { catchError, Observable } from 'rxjs';
  
  @Injectable()
  export class ErrorInterceptor implements HttpInterceptor {
    constructor(private toastr: ToastrService) {}
  
    intercept(
      req: HttpRequest<any>,
      next: HttpHandler
    ): Observable<HttpEvent<any>> {
      return next.handle(req).pipe(
        catchError((err) => {
          if (err instanceof HttpErrorResponse) {
            if (err.status === 0) {
              this.toastr.error("Data kon niet worden opgehaald, omdat er geen reactie van de server was.");
            }
            else {
              this.toastr.error(`Er is een fout opgetreden: \r\n ${err.error}`);
            }
          }
          throw err; // rethrow
        })
      );
    }
  }