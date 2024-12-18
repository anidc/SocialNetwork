import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { HttpInterceptorFn } from '@angular/common/http';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('Token');

  const authReq = token
    ? req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`),
      })
    : req;

  return next(authReq).pipe(
    catchError((error) => {
      if (error.status === 401) {
        // Remove token from local storage on 401 Unauthorized
        localStorage.removeItem('Token');
      }
      return throwError(() => error);
    })
  );
};
