import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { TokenService } from '../../services/token.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent {
  constructor(public tokenService: TokenService, private accountService: AccountService) {}

  logOut() {
    this.accountService.logout();
  }
}
