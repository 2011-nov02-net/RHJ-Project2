import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

// import dev environment
import { environment} from '../../environments/environment';
// need to be added
import { environmentProd} from '../../environments/environment.prod';
import { fromEventPattern, Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Trade } from '../interfaces/trade'
import { Auction } from '../interfaces/auction'

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
  getUserCards() 
  {
    // userId hard coded, replce with login
    return this.http.get(`${this.baseUrl}/users/cus2/cards`);
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
  
}
