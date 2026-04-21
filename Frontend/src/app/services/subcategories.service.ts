import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { SubCategoryDto } from '../models/subcategory.model';

@Injectable({
  providedIn: 'root',
})
export class SubCategoriesService {
  private base = `${environment.apiUrl}/subcategories`;

  constructor(private http: HttpClient) { }

  public getAll(): Observable<SubCategoryDto[]> {
    return this.http.get<SubCategoryDto[]>(this.base).pipe(
      catchError((error: HttpErrorResponse) => throwError(() => new Error('Something went wrong'))
      ));
  }

  public getById(id: string): Observable<SubCategoryDto> {
    return this.http.get<SubCategoryDto>(`${this.base}/${id}`).pipe(
      catchError((error: HttpErrorResponse) => throwError(() => new Error('Something went wrong'))
      ));
  }
}
