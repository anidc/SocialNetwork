import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TokenResponse } from '../interfaces/token';

@Injectable({ providedIn: 'root' })
export class AccountService {
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

  register(
    username: string,
    password: string,
    email: string
  ): Observable<string> {
    return this.http.post<string>(
      `${environment.serverUrl}/api/account/register`,
      {
        username,
        password,
        email,
      }
    );
  }
}
