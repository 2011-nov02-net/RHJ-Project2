import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  email: string = "Login";
  role: string = "0";
  id: string = "";
  currency: number = 0;
  loggedIn: boolean = false;

  constructor() { }

  ngOnInit(): void {
    this.email = JSON.parse(localStorage.getItem('email') || '{}');
    this.role = JSON.parse(localStorage.getItem('role') || '{}');
    this.id = JSON.parse(localStorage.getItem('id') || '{}');
    this.currency = JSON.parse(localStorage.getItem('currency') || '{}');

    if(this.email !== "Login" || "") {
      this.loggedIn = true;
    }
  }

}
