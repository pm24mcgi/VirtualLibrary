import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BookService } from 'src/app/services/book.service';
import { Book } from '../../shared/models/book';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.scss'],
})
export class LibraryComponent {
  books: Book[];

  constructor(private bookService: BookService, private router: Router) {
    this.bookService.getBooks().subscribe((books) => {
      this.books = books;
    });
  }

  create(): void {
    this.router.navigateByUrl('create')
  }

  edit(): void {
    this.router.navigateByUrl('edit')
  }

  detail(): void {
    this.router.navigateByUrl('detail')
  }
}
