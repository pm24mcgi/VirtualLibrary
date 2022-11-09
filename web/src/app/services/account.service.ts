import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { UserLogin } from '../shared/models/login';
import { UserRegister } from '../shared/models/register';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private _token: string;

  constructor(private http: HttpClient) {}

  public get isLoggedIn(): boolean {
    return this.token != null && this.token?.length > 1;
  }

  public get token(): string {
    return this._token;
  }

  logout() {
    this._token = '';
  }

  login(userLogin: UserLogin): Observable<string> {
    return this.http
      .post(`${environment.apiUrl}/account/login`, userLogin, {
        responseType: 'text',
      })
      .pipe(
        tap((response) => {
          this._token = response;
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
