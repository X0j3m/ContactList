import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { TokenDto } from '../models/token.model';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  public loggedIn = signal<boolean>(this.isLoggedIn());

  private base = `${environment.apiUrl}/login`;
  private jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient) { }

  public login(): Observable<TokenDto> {
    return this.http.get<TokenDto>(this.base).pipe(
      tap(token => {
        this.saveToken(token.token);
        this.setLoggendIn();
        }
      ),
      catchError((error: HttpErrorResponse) => {
        console.error(`Error: ${error.status}`);
        return throwError(() => new Error('Something went wrong'));
      })
    );
  }

  public logout() {
    this.removeToken();
    this.setLoggendIn();
  }

  public setLoggendIn() {
    this.loggedIn.set(this.isLoggedIn());
  }

  public isLoggedIn(): boolean {
    var token = localStorage.getItem('token');
    if (token != null && !this.isTokenExpired(token)) {
      return true;
    }
    this.removeToken();
    return false;
  }

  private saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  private removeToken() {
    localStorage.removeItem('token');
  }

  private isTokenExpired(token: string): boolean {
    return this.jwtHelper.isTokenExpired(token);
  }
}
