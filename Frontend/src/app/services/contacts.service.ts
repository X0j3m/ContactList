import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, throwError, forkJoin, of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { ContactModel, ContactDto, CreateContactDto, UpdateContactDto } from '../models/contact.model';
import { CategoryDto } from '../models/category.model'
import { CategoriesService } from '../services/categories.service';
import { SubCategoriesService } from './subcategories.service';

@Injectable({
  providedIn: 'root',
})
export class ContactsService {
  private base = `${environment.apiUrl}/contacts`;
  private jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient,
    private categoriesService: CategoriesService,
    private subcategoriesService: SubCategoriesService) { }

  private authOptions() {
    const token = localStorage.getItem('token');
    if (!token) return {};
    return { headers: { Authorization: `Bearer ${token}` } };
  }

  public getAll(): Observable<ContactModel[]> {
    return this.http.get<ContactDto[]>(this.base).pipe(
      switchMap((dtos: ContactDto[]) => {
        if (dtos.length === 0) return of([]);
        const observables = dtos.map(dto => this.mapDtoToContact(dto));
        return forkJoin(observables);
      }),
      catchError((error: HttpErrorResponse) => throwError(() => new Error('Something went wrong')))
    );
  }

  public getById(id: string): Observable<ContactModel> {
    return this.http.get<ContactDto>(`${this.base}/${id}`).pipe(
      switchMap((dto: ContactDto) => this.mapDtoToContact(dto)),
      catchError((error: HttpErrorResponse) => throwError(() => new Error('Something went wrong')))
    );
  }

  public create(contact: CreateContactDto): Observable<CreateContactDto> {
    return this.http.post<CreateContactDto>(this.base, contact, this.authOptions()).pipe(
      catchError((error: HttpErrorResponse) => throwError(() => new Error('Something went wrong'))
    ));
  }

  public update(id: string, contact: UpdateContactDto): Observable<UpdateContactDto> {
    return this.http.put<any>(`${this.base}/${id}`, contact, this.authOptions()).pipe(
      catchError((error: HttpErrorResponse) => throwError(() => new Error('Something went wrong'))
    ));
  }

  public delete(id: string): Observable<ContactDto> {
    return this.http.delete<ContactDto>(`${this.base}/${id}`, this.authOptions()).pipe(
      catchError((error: HttpErrorResponse) => throwError(() => new Error('Something went wrong'))
    ));
  }

  private mapDtoToContact(dto: ContactDto): Observable<ContactModel> {
    if (dto.categoryId != null && dto.subcategoryId != null) {
      return forkJoin({
        category: this.categoriesService.getById(dto.categoryId),
        subcategory: this.subcategoriesService.getById(dto.subcategoryId)
      }).pipe(
        map(({ category, subcategory }) => {
          const contact: ContactModel = {
            id: dto.id,
            name: dto.name,
            surname: dto.surname,
            email: dto.email,
            password: dto.password,
            phone: dto.phone,
            birthDate: dto.birthDate,
            category: category.name,
            subcategory: subcategory.name
          };
          return contact;
        })
      );
    } else {
      const contact: ContactModel = {
            id: dto.id,
            name: dto.name,
            surname: dto.surname,
            email: dto.email,
            password: dto.password,
            phone: dto.phone,
            birthDate: dto.birthDate,
            category: 'inne',
            subcategory: dto.customSubCategory
          };
      return of(contact);
    }
  }
}
