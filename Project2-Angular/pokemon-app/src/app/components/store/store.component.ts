import { Component, OnInit } from '@angular/core';

import { BackendService } from '../../services/backend.service';
//import {Pack} from '../../interfaces/pack';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.css']
})
export class StoreComponent implements OnInit {

    packs: any;

  constructor(private backEndService: BackendService  ) { }

  ngOnInit(): void {
    //this.backEndService.getStoreItems().subscribe((data) => { this.packs  = data; })
  }

}
