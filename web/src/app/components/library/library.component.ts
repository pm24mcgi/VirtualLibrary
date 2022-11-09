import { Component } from '@angular/core';
import { BookService } from 'src/app/services/book.service';
import { Book } from '../../shared/models/book';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.scss'],
})
export class LibraryComponent {
  books: Book[];

  constructor(private bookService: BookService) {
    this.bookService.getBooks().subscribe((books) => {
      this.books = books;
    });
  }
}
