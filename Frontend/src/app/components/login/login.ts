import { Component, signal } from '@angular/core';
import { LoginService, Token } from '../../services/login.service';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
})
export class Login {
  public loggedIn = signal(false);

  constructor(private loginService: LoginService) {
    this.loggedIn.set(this.loginService.isLoggedIn());
  }

  login() {
    this.loginService.login().subscribe({
      next: (res: Token) => {
        this.loggedIn.set(this.loginService.isLoggedIn());
        return;
      },
      error: (err: any) => {
        alert("Something went wrong");
      }
    });
  }
}
