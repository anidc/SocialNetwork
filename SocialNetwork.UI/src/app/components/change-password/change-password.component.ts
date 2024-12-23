import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { UpdatePassword } from '../../interfaces/update-password';
import { ChangePassword } from '../../interfaces/change-password';

@Component({
  selector: 'app-change-password',
  standalone: false,

  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css',
})
export class ChangePasswordComponent {
  changePasswordForm!: FormGroup;

  isLoading: boolean = false;

  get currentPasswordControl() {
    return this.changePasswordForm.get('currentPassword');
  }
  get newPasswordControl() {
    return this.changePasswordForm.get('newPassword');
  }

  get confirmPasswordControl() {
    return this.changePasswordForm.get('confirmPassword');
  }

  constructor(
    private readonly formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.initChangePasswordForm();
  }
  ngOnInit() {
    if (!this.accountService.isUserAuthenticated()) {
      this.router.navigate(['/home']);
    }
  }

  initChangePasswordForm() {
    this.changePasswordForm = this.formBuilder.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    if (this.changePasswordForm.valid) {
      this.isLoading = true;

      if (
        this.newPasswordControl?.invalid ||
        this.confirmPasswordControl?.invalid
      ) {
        this.newPasswordControl?.markAsTouched();
        this.confirmPasswordControl?.markAsTouched();
        this.isLoading = false;
        return;
      }
      if (
        this.newPasswordControl?.value !== this.confirmPasswordControl?.value
      ) {
        this.toastr.error('Passwords do not match');
        this.isLoading = false;
        return;
      } else {
        this.accountService
          .changePassword(this.changePasswordForm.value as ChangePassword)
          .pipe(
            finalize(() => {
              this.isLoading = false;
            })
          )
          .subscribe({
            next: () => {
              this.toastr.success('Password updated successfully');
              this.router.navigate(['/home']);
            },
            error: (error) => {
              this.toastr.error(error.error);
            },
          });
      }
    }
  }
}
