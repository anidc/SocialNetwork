import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'input-button',
  standalone: false,

  templateUrl: './input-button.component.html',
  styleUrl: './input-button.component.css',
})
export class InputButtonComponent {
  @Input() label: string = '';
  @Input() disabled: boolean = false;

  @Output() clicked: EventEmitter<void> = new EventEmitter<void>();

  onClick() {
    this.clicked.emit();
  }
}
