import { Component, OnInit } from '@angular/core';

import { BackendService } from '../../services/backend.service';
import {Pack} from '../../interfaces/pack';
import {Order} from '../../interfaces/order';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.css']
})
export class StoreComponent implements OnInit {

    packs: Pack[] | any;
    orders: Order[] | any;

  constructor(private backEndService: BackendService  ) { }

  ngOnInit(): void {
    this.getStorePacks();
  }

  getStorePacks(): void{
    this.backEndService.getStorePacks().subscribe(packs => { this.packs  = packs; })
  }
  sendOrder(input: number): void{

  }
}