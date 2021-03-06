import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { Location} from '@angular/common';

import { User} from '../../interfaces/user';
import { Login} from '../../interfaces/login';
import { ProgressBarComponent} from '../progress-bar/progress-bar.component';

// need backend services
import { BackendService} from '../../services/backend.service';
import { Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

 
  id: any ='';
  first: any = '';
  last: any = '';
  email: any;
  role: any = '';
  numPacksPurchased: any = 0;
  currency: any = 0;
  loggedIn: boolean = false;

  user: User | any;
  loadProgress =0;
  loadTotal = 100;


  constructor( private route:ActivatedRoute, private backendService:BackendService, private location:Location) {    
  }

  ngOnInit(): void {
    this.email = this.route.snapshot.params.id;
    this.id = JSON.parse(localStorage.getItem('id') || '{}');
    this.first = JSON.parse(localStorage.getItem('first') || '{}');
    this.last = JSON.parse(localStorage.getItem('last') || '{}');
    this.role = JSON.parse(localStorage.getItem('role') || '{}');
    this.numPacksPurchased = JSON.parse(localStorage.getItem('numPacksPurchased') || '{}');
    this.currency = JSON.parse(localStorage.getItem('currency') || '{}');
    this.getUser();
    this.LoadProgress();
  }

  getUser(): void {
    this.backendService.getUserByEmail(this.email).subscribe(data => this.user = data);
  }

  async LoadProgress()
  {
    while(this.loadProgress!= 100)
    {
      await this.delay(300); 
      this.loadProgress += 2;
      console.log(this.loadProgress);
    }
     
  }

  delay(ms:number)
  {
    return new Promise( resolve => setTimeout(resolve,ms));
  }

  

  

}
