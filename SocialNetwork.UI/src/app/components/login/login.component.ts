import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/User.service';

@Component({
  selector: 'app-login',
  standalone: false,
  
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  constructor(private userService: UserService) { }
  ngOnInit() { }
  onSubmit() {
    this.userService.login(this.loginForm.value.username!, this.loginForm.value.password!).subscribe(response => {
      console.log(response);
      // localStorage.setItem('token', response.token);
    }
    );
  }

}
