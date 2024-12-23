import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { TokenResponse } from '../interfaces/token';
import { User } from '../interfaces/user';
import { UpdatePassword } from '../interfaces/update-password';
import { ChangePassword } from '../interfaces/change-password';

@Injectable({ providedIn: 'root' })
export class AccountService {
  CurrentUser$: BehaviorSubject<User | null> = new BehaviorSubject<User | null>(
    null
  );

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(
      `${environment.serverUrl}/api/account/login`,
      {
        username,
        password,
      }
    );
  }

  isUserAuthenticated(): boolean {
    return localStorage.getItem('Token') != null;
  }

  register(
    username: string,
    password: string,
    email: string
  ): Observable<string> {
    return this.http.post(
      `${environment.serverUrl}/api/account/register`,
      {
        username,
        password,
        email,
      },
      { responseType: 'text' }
    );
  }

  getCurrentUser(): Observable<User> {
    return this.http.get<User>(`${environment.serverUrl}/api/account/me`).pipe(
      tap({
        next: (user: User) => {
          this.CurrentUser$.next(user);
        },
        error: (error) => {
          this.CurrentUser$.next(null);
        },
      })
    );
  }

  logout() {
    localStorage.removeItem('Token');
    this.CurrentUser$.next(null);
  }

  resetPassword(email: string): Observable<string> {
    return this.http.post(
      `${environment.serverUrl}/api/account/reset-password`,
      {
        email,
      },
      { responseType: 'text' }
    );
  }

  updatePassword(updatePassword: UpdatePassword): Observable<UpdatePassword> {
    return this.http.post<UpdatePassword>(
      `${environment.serverUrl}/api/account/update-password`,
      updatePassword
    );
  }

  changePassword(changePassword: ChangePassword): Observable<ChangePassword> {
    return this.http.post<ChangePassword>(
      `${environment.serverUrl}/api/account/change-password`,
      changePassword,
      { responseType: 'json' }
    );
  }
}
