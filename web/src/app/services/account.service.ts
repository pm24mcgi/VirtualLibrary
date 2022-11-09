import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { UserLogin } from '../shared/models/login';
import { UserRegister } from '../shared/models/register';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private _token: string;
  loggedIn: boolean;


  constructor(private http: HttpClient, private tokenService: TokenService) {}

  public get isLoggedIn(): boolean {
    return this.token != null && this.token?.length > 1;
  }

  public get token(): string {
    return this._token;
  }

  logout() {
    this._token = '';
    this.tokenService.removeToken();
    this.loggedIn = false;
  }

  login(userLogin: UserLogin): Observable<string> {
    return this.http
      .post(`${environment.apiUrl}/account/login`, userLogin, {
        responseType: 'text',
      })
      .pipe(
        tap((response) => {
          this._token = response;
          this.tokenService.setToken(this._token);
          return this.loggedIn = true;
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
