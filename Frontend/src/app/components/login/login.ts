import { Component, signal } from '@angular/core';
import { LoginService, } from '../../services/login.service';
import { TokenDto } from '../../models/token.model';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
})
export class Login {
  constructor(private loginService: LoginService) {
  }

  public loggedIn(): boolean {
    return this.loginService.loggedIn();
  }

  login() {
    this.loginService.login().subscribe({
      next: (res: TokenDto) => {
        return;
      },
      error: (err: any) => {
        alert("Something went wrong");
      }
    });
  }

  logout() {
    this.loginService.logout();
  }
}
