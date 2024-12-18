import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-menu',
  standalone: false,

  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css',
})
export class MenuComponent {
  isAuthenticated = false;

  constructor(private router: Router, public accountService: AccountService) {
    this.isAuthenticated = this.accountService.isUserAuthenticated();
  }

  ngOnInit(): void {
    this.accountService.getCurrentUser().subscribe();
  }

  logout() {
    this.accountService.logout();
    this.router.navigate(['/login']);
  }
}
