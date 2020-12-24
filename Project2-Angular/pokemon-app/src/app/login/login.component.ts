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
    checked:false,    
  }
    
  user:User ={
    userId:'',
    first:'',
    last:'',
    email:'',
    userRole:'',
    numPacksPurchased:0,
    currencyAmount:0,
  }

  guests:User[] | null = null
  
  constructor(private backendService:BackendService, private router:Router) { }

  ngOnInit(): void {
    // guests login
    // this.backendService.getUsers().subscribe((data) => { this.guests = data;});
  }


  SubmitLogin()
  {
    // send login info to backend and fectch customer id
    this.backendService.getUserByEmail(this.login.email).subscribe(data =>  console.log(data));
    this.backendService.getUserByEmail(this.login.email).subscribe((data) => { 
      this.user = data;
      localStorage.setItem('id', JSON.stringify({ id: data.userId}));
      localStorage.setItem('first', JSON.stringify({ first: data.first}));
      localStorage.setItem('last', JSON.stringify({ last: data.last}));
      localStorage.setItem('email', JSON.stringify({ email: data.email}));
      localStorage.setItem('role', JSON.stringify({ role: '1'}));
      localStorage.setItem('numPacks', JSON.stringify({ numPacksPurchased: data.numPacksPurchased}));     
      localStorage.setItem('currency', JSON.stringify({ currency: data.currencyAmount}));
     });
    // this is another way to route, if routerlink works, comment this line 
    // this.router.navigate(['/user/' + this.user.userId]);
  }


}
