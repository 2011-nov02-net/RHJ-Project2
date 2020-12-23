import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

export class NavComponent implements OnInit {

  email: any = "Login";
  role: any = "0";
  id: any = "";
  currency: any = 0;
  loggedIn: boolean = false;

  constructor() {
    
  }

  ngOnInit(): void {
    this.email = JSON.parse(localStorage.getItem('email') || '{}');
    this.role = JSON.parse(localStorage.getItem('role') || '{}');
    this.id = JSON.parse(localStorage.getItem('id') || '{}');
    this.currency = JSON.parse(localStorage.getItem('currency') || '{}');

    if(this.email !== "Login") {
      this.loggedIn = true;
    }

    if(this.email.email == null) {
      this.email.email = "Login";
      this.loggedIn = false;
    }
  }

  logOut():void {
    localStorage.removeItem('email');
    localStorage.removeItem('role');
    localStorage.removeItem('id');
    localStorage.removeItem('currency');
    this.loggedIn = false;
    this.ngOnInit();
  }

}
