import { Component, OnInit } from '@angular/core';
import { Login} from '../interfaces/login';
//import { RouterModule} from '@angular/route';

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

  constructor() { }

  ngOnInit(): void {
  }

  SubmitLogin()
  {
    // send login info to backend and fectch customer id
    //   

  }

}
