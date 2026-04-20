import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';

export interface Token { token: string; }

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private base = `${environment.apiUrl}/login`;
  private jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient) { }

  public login(): Observable<Token> {
    return this.http.get<Token>(this.base).pipe(
      tap(token => {
        this.saveToken(token.token);
        }
      ),
      catchError((error: HttpErrorResponse) => {
        console.error(`Error: ${error.status}`);
        return throwError(() => new Error('Something went wrong'));
      })
    );
  }

  public isLoggedIn(): boolean {
    var token = localStorage.getItem('token');
    if (token != null && !this.isTokenExpired(token)) {
      return true;
    }
    return false;
  }

  private saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  private isTokenExpired(token: string): boolean {
    return this.jwtHelper.isTokenExpired(token);
  }
}
