import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  navToggle: boolean;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    console.log('navToggle', this.navToggle);
    if (this.accountService.loggedIn === true) {
      this.navToggle = true;
    } else {
      this.navToggle = false;
    }
  }
}
