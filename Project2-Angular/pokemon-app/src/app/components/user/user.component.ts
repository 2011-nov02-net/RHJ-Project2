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

  @Input() login!:Login;
  @Input() user!:User;
  
  
  constructor( private route:ActivatedRoute, private backendService:BackendService, private location:Location) {    
  }

  ngOnInit(): void {
    console.log(history.state.login);
    console.log(this.login)
  }

  getOneUser():void{
    const id = this.route.snapshot.paramMap.get('id')!;
    this.backendService.getUserById(id).subscribe( user => this.user = user)
  }

  goNack(): void{
    this.location.back();
  }

}
