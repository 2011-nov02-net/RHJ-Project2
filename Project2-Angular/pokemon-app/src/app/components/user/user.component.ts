import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { Location} from '@angular/common';

import { User} from '../../interfaces/user';
import { Login} from '../../interfaces/login';
// need backend services
import { BackendService} from '../../services/backend.service';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  @Input() currentUser!:User;
  @Input() login!:Login;
  
  constructor( private route:ActivatedRoute, private backendService:BackendService, private location:Location) { 
    // set up services
    
  }

  ngOnInit(): void {
    this.currentUser = history.state.loginUser;
    console.log(history.state.loginUser);
    console.log(this.currentUser);
    console.log(history.state.login);
    console.log(this.login)
  }

  getOneUser():void{
    const id = this.route.snapshot.paramMap.get('id')!;
    this.backendService.getUserById(id).subscribe( user => this.currentUser = user)
  }

  goNack(): void{
    this.location.back();
  }

}
