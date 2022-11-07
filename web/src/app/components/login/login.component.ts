import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loggedIn: boolean = false;

  constructor(
    private fb: FormBuilder, 
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loginForm = this.fb.group({
      username: '',
      password: ''
    });
  }


  login() {
    // console.log(this.loginForm.value);
    // this.accountService.login(this.loginForm.get('username').value, this.loginForm.get('password').value).subscribe(response => {
    //   console.log(response);
    //   this.loggedIn = true;
    // this.router.navigateByUrl('/library')
    // }, error => {
    //   console.log(error);
    // })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }
}
