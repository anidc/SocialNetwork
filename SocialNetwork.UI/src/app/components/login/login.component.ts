import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { TokenResponse } from '../../interfaces/token';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,

  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  constructor(
    private userService: AccountService,
    private readonly router: Router
  ) {}
  ngOnInit() {}
  onSubmit() {
    this.userService
      .login(this.loginForm.value.username!, this.loginForm.value.password!)
      .subscribe({
        next: (response: TokenResponse) => {
          localStorage.setItem('token', response.token);

          if (response.token) {
            this.router.navigate(['/home']);
          }
        },
        error: (error) => {
          alert(error.error.toString());
        }
      });
  }
}
