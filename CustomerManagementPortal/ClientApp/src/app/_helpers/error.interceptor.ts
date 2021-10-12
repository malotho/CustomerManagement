import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AccountService } from 'src/app/_services';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => {

          console.log(error)

          //This is a hack - Fluent validation cant be handled on client-side (as far as I know)
          //This is to demonstrate server-side validation
          if ((request.method == "POST" || request.method == "PUT")
            && request.url.includes("/api/categories")) {
            let catErrorsMessage = '';
            let catErrors = error.error.errors;
            catErrorsMessage = catErrors.CategoryCode[0];

            return throwError(catErrorsMessage);
          }

          console.log(request);
          console.log(error);

          let errorMsg = '';
          if (error.error instanceof ErrorEvent) {
            errorMsg = `Error: ${error.error.message}`;
          }
          else {
            console.log('this is server side error');
            errorMsg = `Error Code: ${error.status},  Message: ${error.message}`;
          }
          console.log(errorMsg);
          return throwError(errorMsg);
        })
      )
    //  .pipe(catchError((err: HttpErrorResponse) => {

    //  console.log(err)
    //  console.log(err.status)     
    //  if (err.status === 401) {
    //    // auto logout if 401 response returned from api
    //    this.accountService.logout();
    //  }

    //  const error = err.error.message;
    //  return throwError(error);
    //}))
  }
}
