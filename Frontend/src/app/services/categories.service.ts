import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { CategoryDto } from '../models/category.model';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  private base = `${environment.apiUrl}/categories`;

  constructor(private http: HttpClient) { }

  public getAll(): Observable<CategoryDto[]> {
    return this.http.get<CategoryDto[]>(this.base).pipe(
      catchError((error: HttpErrorResponse) => throwError(() => new Error('Something went wrong'))
    ));
  }

  public getById(id: string): Observable<CategoryDto> {
    return this.http.get<CategoryDto>(`${this.base}/${id}`).pipe(
      catchError((error: HttpErrorResponse) => throwError(() => new Error('Something went wrong'))
    ));
  }
}
