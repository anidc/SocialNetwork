import { Injectable } from '@angular/core';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isAuthenticated = false;
  private authSecretKey = 'Token';

  constructor(private readonly accountService: AccountService) {
    this.isAuthenticated = !!localStorage.getItem(this.authSecretKey);
  }

  async login(username: string, password: string): Promise<boolean> {
    try {
      const response = await this.accountService.login(username, password).toPromise();
      if (response) {
        localStorage.setItem(this.authSecretKey, response.token);
        this.isAuthenticated = true;
        return true;
      }
      return false;
    } catch (error) {
      console.error('Login failed', error);
      return false;
    }
  }

  isAuthenticatedUser(): boolean {
    return this.isAuthenticated;
  }

  logout() {
    localStorage.removeItem(this.authSecretKey);
    this.isAuthenticated = false;
  }
}
