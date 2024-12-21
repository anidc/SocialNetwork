import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { UpdatePassword } from '../../interfaces/update-password';

@Component({
  selector: 'app-reset-password',
  standalone: false,

  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css',
})
export class ResetPasswordComponent {
  resetPasswordForm!: FormGroup;

  isLoading: boolean = false;

  get emailControl() {
    return this.resetPasswordForm.get('email');
  }

  get passwordControl() {
    return this.resetPasswordForm.get('password');
  }

  get confirmPasswordControl() {
    return this.resetPasswordForm.get('confirmPassword');
  }

  get tokenControl() {
    return this.resetPasswordForm.get('token');
  }

  constructor(
    private readonly formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ) {
    this.initResetPassword();

    this.route.queryParams.subscribe((params) => {
      var emailParam = params['email'];
      var tokenParam = params['token'];

      this.resetPasswordForm.patchValue({
        email: emailParam,
        token: tokenParam,
      });

      if (emailParam || tokenParam) {
        this.resetPasswordForm
          .get('token')
          ?.addValidators([Validators.required]);
        this.resetPasswordForm
          .get('password')
          ?.addValidators([Validators.required, Validators.minLength(6)]);
        this.resetPasswordForm
          .get('confirmPassword')
          ?.addValidators([Validators.required, Validators.minLength(6)]);

        this.resetPasswordForm.updateValueAndValidity();
      }
    });
  }

  ngOnInit() {
    if (this.accountService.isUserAuthenticated()) {
      this.router.navigate(['/home']);
    }
  }

  initResetPassword() {
    this.resetPasswordForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', []],
      confirmPassword: ['', []],
      token: ['', []],
    });
  }

  onSubmit() {
    this.isLoading = true;

    if (this.emailControl?.value && !this.tokenControl?.value) {
      if (this.emailControl?.invalid) {
        this.emailControl.markAsTouched();
        return;
      }

      this.accountService
        .resetPassword(this.resetPasswordForm.value.email!)
        .subscribe({
          next: (response) => {
            this.toastr.info('Please check your email');
            this.router.navigate(['/login']);
          },
          error: (error) => {
            console.log(error);
          },
          complete: () => {
            this.isLoading = false;
          },
        });
    } else {
      if (
        this.passwordControl?.invalid ||
        this.confirmPasswordControl?.invalid
      ) {
        this.passwordControl?.markAsTouched();
        this.confirmPasswordControl?.markAsTouched();
        return;
      }

      if (this.passwordControl?.value !== this.confirmPasswordControl?.value) {
        this.confirmPasswordControl?.setErrors({ passwordMismatch: true });
        return;
      }
      this.accountService
        .updatePassword(this.resetPasswordForm.value as UpdatePassword)
        .subscribe({
          next: (response) => {
            this.toastr.success('Password updated successfully');
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
}
