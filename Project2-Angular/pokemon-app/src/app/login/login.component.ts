import { Component, OnInit } from '@angular/core';
import { Router} from '@angular/router';


import { Login} from '../interfaces/login';
import { User } from '../interfaces/user';
import { BackendService} from '../services/backend.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login:Login = {    
    email:'',
    password:'',    
  }
    
  user:User ={
    userId:'',
    first:'',
    last:'',
    email:'',
    // extras can be removed
    //userRole:string;
    numPacksPurchased:0,
    // use | currency in an html element
    currencyAmount:0,
  }
  
  constructor(private backendService:BackendService, private router:Router) { }

  ngOnInit(): void {
  }


  SubmitLogin()
  {
    // send login info to backend and fectch customer id
    console.log(this.login); 
    console.log(this.user);
    this.backendService.getUserByEmail(this.login.email).subscribe(data =>  console.log(data));
    this.backendService.getUserByEmail(this.login.email).subscribe((data) => { this.user = data; });

    // this is another way to route, if routerlink works, comment this line 
    this.router.navigate(['/user/' + this.user.userId]);
  }

}
