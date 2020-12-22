import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

// import dev environment
import { environment} from '../../environments/environment';
// need to be added
import { environmentProd} from '../../environments/environment.prod';
import { fromEventPattern, Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { User } from '../interfaces//user';
import { Card } from '../interfaces/card';
import { Trade } from '../interfaces/trade'
import { Auction } from '../interfaces/auction'
import { Pack } from '../interfaces/pack'
import { Order } from '../interfaces/order'

@Injectable({
  providedIn: 'root'
})
export class BackendService {
  // to be replaced by app service url
  //private baseUrl =  'https://localhost:44301/api';
  private baseUrl = `${environment.baseUrl}`;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http:HttpClient) { }
  
  // promise -> observable  

  getUserByEmail(email:string): Observable<User>
  {
    return this.http.get<User>(this.baseUrl + "/users/emails/" + email);

  }

  getUserCards():Observable<Card[]> 
  {
    // userId hard coded, replce with login
    return this.http.get<Card[]>(`${this.baseUrl}/users/cus2/cards`);
  }

  getTrades(): Observable<Trade[]> {
    return this.http.get<Trade[]>(this.baseUrl + '/trades');
  }

  getTradeById(id:string): Observable<Trade> {
    return this.http.get<Trade>(this.baseUrl + '/trades/' + id);
  }

  getAuctions(): Observable<Auction[]> {
    return this.http.get<Auction[]>(this.baseUrl + '/auctions');
  }

  getStorePacks(): Observable<Pack[]>
  {
    return this.http.get<Pack[]>(`${this.baseUrl}/store`);
  }
  postOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(this.baseUrl + '/order', order);
  }

}