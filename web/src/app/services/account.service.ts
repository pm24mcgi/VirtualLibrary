import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { UserLogin } from '../shared/models/login';
import { UserRegister } from '../shared/models/register';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {

  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
    private router: Router
    ) {}

  logout() {
    this.tokenService.token$.next('');
    this.router.navigateByUrl('splash')
  }

  login(userLogin: UserLogin): Observable<string> {
    return this.http
      .post(`${environment.apiUrl}/account/login`, userLogin, {
        responseType: 'text',
      })
      .pipe(
        tap((response) => {
          this.tokenService.token$.next(response);
        })
      );
  }

  register(userRegister: UserRegister) {
    return this.http.post(
      `${environment.apiUrl}/account/register`,
      userRegister
    );
  }
}
