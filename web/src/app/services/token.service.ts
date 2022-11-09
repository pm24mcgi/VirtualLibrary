import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  setToken(jwt: string) {
    this.removeToken();
    return localStorage.setItem('jwt', jwt);
  }

  getToken() {
    return localStorage.getItem('jwt');
  }

  removeToken() {
    return localStorage.removeItem('jwt')
  }
}
