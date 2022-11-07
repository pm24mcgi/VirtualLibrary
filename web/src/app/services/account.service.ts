import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private http: HttpClient) { }

  login(username: string, password: string){
    return this.http.post(`${environment.apiUrl}/account/login`, {username, password});
  }

  logout() {}

  register(email: string, password: string, role: string){
    return this.http.post(`${environment.apiUrl}/account/register`, {email, password, role});
  }
}
