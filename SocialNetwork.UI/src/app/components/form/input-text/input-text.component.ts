import { Component, Input } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'input-text',
  standalone: false,
  templateUrl: './input-text.component.html',
  styleUrls: ['./input-text.component.css'],
})
export class InputTextComponent {
  @Input() label: string = ''; // Label for the input field
  @Input() placeholder: string = ''; // Placeholder for the input
  @Input() formGroup!: FormGroup; // Parent form group
  @Input() controlName!: string; // Name of the form control
  @Input() type: string = 'text'; // Input type

  get formControl(): AbstractControl | null {
    return this.formGroup.get(this.controlName);
  }

  get errorMessages(): string[] {
    if (
      !this.formControl ||
      !this.formControl.errors ||
      !this.formControl.touched
    ) {
      return [];
    }

    const errors = this.formControl.errors;
    const messages: string[] = [];

    if (errors['required']) {
      messages.push('This field is required.');
    }
    if (errors['minlength']) {
      messages.push(
        `Minimum length is ${errors['minlength'].requiredLength} characters.`
      );
    }
    if (errors['maxlength']) {
      messages.push(
        `Maximum length is ${errors['maxlength'].requiredLength} characters.`
      );
    }
    if (errors['email']) {
      messages.push('Please enter a valid email address.');
    }
    if (errors['pattern']) {
      messages.push('Invalid format.');
    }

    return messages;
  }
}
