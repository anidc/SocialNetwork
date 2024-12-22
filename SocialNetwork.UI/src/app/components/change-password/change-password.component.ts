import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-change-password',
  standalone: false,

  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css',
})
export class ChangePasswordComponent {
  changePasswordForm!: FormGroup;

  isLoading: boolean = false;

  constructor(
    private readonly formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}
  ngOnInit() {
    if (!this.accountService.isUserAuthenticated()) {
      this.router.navigate(['/home']);
    }
  }

  initChangePasswordForm() {
    this.changePasswordForm = this.formBuilder.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.changePasswordForm.valid) {
      this.isLoading = true;
    }
  }
}
