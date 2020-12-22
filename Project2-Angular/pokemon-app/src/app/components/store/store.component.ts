import { Component, OnInit } from '@angular/core';
import { fromEventPattern, Observable } from 'rxjs';

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
    orders: Order[] |any;

  constructor(private backEndService: BackendService  ) { }

  ngOnInit(): void {
    this.getStorePacks();
    //this.orders = this.initialOrders();
    this.getOrders();
  }

  getStorePacks(): void{
    this.backEndService.getStorePacks().subscribe(packs => { this.packs  = packs; });
  }
  getOrders():void {
    this.backEndService.getOrders().subscribe(orders => { this.orders  = orders; });
  }
  // initialOrders():Observable<Order[]>{
  //   return this.backEndService.getOrders();
  // }
  sendOrder(order: Order): void{
    this.backEndService.postOrder(order);
  }
}