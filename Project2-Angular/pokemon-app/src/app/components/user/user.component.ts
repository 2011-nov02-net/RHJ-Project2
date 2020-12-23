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

  login!:Login;
  user!:User;
  test$!:Observable<User>;
  
  
  constructor( private route:ActivatedRoute, private backendService:BackendService, private location:Location) {    
  }

  ngOnInit(): void {
    this.getOneUser();
    console.log(history.state.login);
    console.log(this.login)
  }

  getOneUser():void{
    const id = this.route.snapshot.paramMap.get('id')!;
    this.backendService.getUserById(id).subscribe( user => this.user = user)
  }

  goBack(): void{
    this.location.back();
  }

}
