import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { Location} from '@angular/common';

import { User} from '../../interfaces/user';
import { Login} from '../../interfaces/login';

// need backend services
import { BackendService} from '../../services/backend.service';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  //user!:User;

  id: any ='';
  first: any = '';
  last: any = '';
  email: any = '';
  role: any = '';
  numPacksPurchased: any = 0;
  currency: any = 0;
  loggedIn: boolean = false;

  constructor( private route:ActivatedRoute, private backendService:BackendService, private location:Location) {    
  }

  ngOnInit(): void {
    this.id = JSON.parse(localStorage.getItem('id') || '{}');
    this.first = JSON.parse(localStorage.getItem('first') || '{}');
    this.last = JSON.parse(localStorage.getItem('last') || '{}');
    this.email = JSON.parse(localStorage.getItem('email') || '{}');
    this.role = JSON.parse(localStorage.getItem('role') || '{}');
    this.numPacksPurchased = JSON.parse(localStorage.getItem('numPacksPurchased') || '{}');
    this.currency = JSON.parse(localStorage.getItem('currency') || '{}');
  }

  // getOneUser():void{
  //   const id = this.route.snapshot.paramMap.get('id')!;
  //   this.backendService.getUserById(id).subscribe( user => this.user = user)
  // }

  goBack(): void{
    this.location.back();
  }

}
