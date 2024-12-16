import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { TokenResponse } from '../../interfaces/token';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';

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
    private authGuard: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {}
  ngOnInit() 
  {
    if (this.authGuard.isAuthenticatedUser()) {
      this.router.navigate(['/home']);
    }

  }
  async onSubmit() {
    try {
      var response = await this.authGuard.login(
        this.loginForm.value.username!,
        this.loginForm.value.password!
      );
      if (response) {
        this.toastr.success('Login successful');
        this.router.navigate(['/home']);
      } else {
        this.toastr.error('Invalid username or password');
      }
    } catch (error) {
      this.toastr.error('An error occurred during login. Please try again later.');
    }
  }
}
