import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder, 
    private accountService: AccountService
  ) {}

  ngOnInit() {
    this.registerForm = this.fb.group({
      username: '',
      password: '',
      role: ''
    });
  }


  register() {
    // this.accountService.register(this.registerForm.get('username').value, this.registerForm.get('password').value, this.registerForm.get('role').value).subscribe(response => {
    //   console.log(response);
    // }, error => {
    //   console.log(error);
    // })
  }

}
