import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: false,

  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  registerForm!: FormGroup;

  isLoading: boolean = false;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly accountService: AccountService,
    private readonly router: Router,
    private toastr: ToastrService
  ) {
    this.initRegisterForm();
  }

  initRegisterForm() {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  register() {
    this.isLoading = true;
    if (this.registerForm.invalid) {
      console.log('Invalid form');
      return;
    }

    this.accountService
      .register(
        this.registerForm.value.username,
        this.registerForm.value.password,
        this.registerForm.value.email
      )
      .subscribe({
        next: (response) => {
          this.toastr.success(response);
          this.router.navigate(['/login']);
        },
        error: (error) => {
          this.toastr.error(error.error);
        },
        complete: () => {
          this.isLoading = false;
        },
      });
  }
}
