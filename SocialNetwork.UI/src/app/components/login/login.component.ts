import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { TokenResponse } from '../../interfaces/token';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';

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

  isLoading: boolean = false;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    if (this.accountService.isUserAuthenticated()) {
      this.router.navigate(['/home']);
    }
  }
  async onSubmit() {
    this.isLoading = true;

    this.accountService
      .login(this.loginForm.value.username!, this.loginForm.value.password!)
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe({
        next: (response: TokenResponse) => {
          localStorage.setItem('Token', response.token);
          this.accountService.getCurrentUser().subscribe();
          this.router.navigate(['/home']);
        },
        error: (error) => {
          this.toastr.error(error.error);
        },
      });
  }
}
