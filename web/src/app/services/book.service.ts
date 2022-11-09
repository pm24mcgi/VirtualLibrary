import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environment';
import { Book } from '../shared/models/book';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ) {}

  getBooks(): Observable<Book[]> {
    const headers = {
      headers: new HttpHeaders().set(
        'Authorization',
        `Bearer ${this.accountService.token}`
      ),
    };

    return this.http.get<Book[]>(`${environment.apiUrl}/books`, headers).pipe(
      map((books: Book[]) => {
        return books.map((book) => {
          book.releaseDate = new Date(book.releaseDate);
          return book;
        });
      })
    );
  }
}
