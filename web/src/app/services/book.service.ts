import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environment';
import { Book } from '../shared/models/book';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  constructor(
    private http: HttpClient,
    private tokenService: TokenService
  ) {}

  getBooks(): Observable<Book[]> {
    const headers = {
      headers: new HttpHeaders().set(
        'Authorization',
        `Bearer ${this.tokenService.getToken()}`
      ),
    };

    return this.http.get<Book[]>(`${environment.apiUrl}/books`, headers).pipe(
      map((books: Book[]) => {
        return books
      })
    );
  }

  getBook() {

  }

  postBook() {
    
  }

  editBook() {
    
  }

  deleteBook() {
    
  }
}
