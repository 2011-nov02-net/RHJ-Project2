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
    user: any;
    qty: number = 0;

  constructor(private backEndService: BackendService  ) { }

  ngOnInit(): void {
    this.getStorePacks();
    //this.orders = this.initialOrders();
    this.getOrders();
    this.user = JSON.parse(localStorage.getItem('id') || '{}');
  }

  getStorePacks(): void{
    this.backEndService.getStorePacks().subscribe(packs => { this.packs  = packs; });
  }
  getOrders():void {
    this.backEndService.getOrders().subscribe(orders => { this.orders  = orders; });
  }

  qtyChange(event:any): void {
    this.qty = event.target.value;
  }
  // initialOrders():Observable<Order[]>{
  //   return this.backEndService.getOrders();
  // }
  //should get Order object from current user and Pack
  sendOrder(pack:string): void{
    let order: Order = {
      orderId: "123",
      userId: this.user.id,
      date: new Date(),
      total: this.qty * 2,
      packId: pack,
      packQty: this.qty
    }

    this.backEndService.postOrder(order).subscribe();
  }
}