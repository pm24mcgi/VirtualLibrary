import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Virtual Library Web';
  books: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getBooks
  }

  getBooks() {
    this.http.get('https://localhost:7188/api/vl').subscribe(response => {
      this.books = response;
    }, error => {
      console.log(error)
    })
  }
}
