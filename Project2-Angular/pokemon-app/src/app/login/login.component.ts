import { Component, OnInit } from '@angular/core';

import { Login} from '../interfaces/login';
import { User } from '../interfaces/user';
import { BackendService} from '../services/backend.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login:Login = 
  {
    email:'',
    password:'',    
  }

  user:User=
  {
    UserId:'',  
    Frist:'',
    Last:'',
    Email:'', 
    UserRole: '',
    NumberPacksPurchased:0, 
    CurrencyAmount: 0 
  }

  constructor(private backendService:BackendService) { }

  ngOnInit(): void {
  }

  SubmitLogin()
  {
    // send login info to backend and fectch customer id
    //  
    console.log(this.login); 
    console.log(this.user);
    this.backendService.getUserByEmail(this.login.email).subscribe(data =>  this.user = data);
    console.log(this.user);
    console.log(this.login);
    // Initials@gmail.com
  }

}
