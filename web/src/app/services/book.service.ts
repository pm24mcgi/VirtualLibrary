import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environment';
import { Book } from '../shared/models/book';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  constructor(private http: HttpClient) {}

  getBooks(): Observable<Book[]> {
    return this.http
    .get<Book[]>(`${environment.apiUrl}/books`)
    .pipe(
      map((books: Book[]) => {
        return books.map((book) => {
          book.releaseDate = new Date(book.releaseDate);
          return book;
        });
      })
    );
  }
}
