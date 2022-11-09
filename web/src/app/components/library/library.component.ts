import { Component, OnInit } from '@angular/core';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.scss'],
})
export class LibraryComponent implements OnInit {
  books = this.getBooks()

  constructor(private bookService: BookService) {}

  ngOnInit(): void {}

  getBooks() {
    const books = this.bookService.getBooks()
    console.log(books)
    return books;
  }
}
