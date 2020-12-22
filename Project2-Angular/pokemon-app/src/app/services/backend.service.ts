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
  //User
  getUsers(): Observable<User[]>
  {
    return this.http.get<User[]>(this.baseUrl + "/users");
  }

  postUser(user: User): Observable<User> {
    return this.http.post<User>(this.baseUrl + '/users', user);
  }

  getUserById(id:string): Observable<User>
  {
    return this.http.get<User>(this.baseUrl + "/users/" + id);

  }

  //not in Usercontroller
  getUserByEmail(email:string): Observable<User>
  {
    return this.http.get<User>(this.baseUrl + "/users/emails/" + email);

  }

  getUserCards():Observable<Card[]> 
  {
    // userId hard coded, replce with login
    return this.http.get<Card[]>(`${this.baseUrl}/users/cus2/cards`);
  }

  postUserCard(id:string, card: Card): Observable<Card> {
    
    return this.http.post<Card>(this.baseUrl + '/users/' + id + '/cards', card);
  }
  getUserCard(id:string, cardId:string):Observable<Card> 
  {
    
    return this.http.get<Card>(`${this.baseUrl}/users/${id}/cards/${cardId}`);
  }

  updateUserById(id:string,user:User):Observable<User>{
    return this.http.put<User>(`${this.baseUrl}/users/${id}`, user);
  }
  //Trades
  getTrades(): Observable<Trade[]> {
    return this.http.get<Trade[]>(this.baseUrl + '/trades');
  }
  postTrade(trade:Trade): Observable<Trade> {
    return this.http.post<Trade>(this.baseUrl + '/trades',trade);
  }
  getTradeById(id:string): Observable<Trade> {
    return this.http.get<Trade>(this.baseUrl + '/trades/' + id);

<<<<<<< HEAD
  postTrade(trade: Trade): Observable<Trade> {
    console.log("Trade");
    return this.http.post<Trade>(this.baseUrl + '/trades', trade);
  }

=======
  }
  updateTradeById(id:string,trade:Trade): Observable<Trade> {
    return this.http.put<Trade>(this.baseUrl + '/trades/' + id,trade);
  }
>>>>>>> f95c5d511b53235a43494c8b03213954f1d8c6e2
  //Auctions
  //does not get auction details
  getAuctions(): Observable<Auction[]> {
    return this.http.get<Auction[]>(this.baseUrl + '/auctions');
  }
  
  postAuction(auction:Auction):Observable<Auction>{
    return this.http.post<Auction>(this.baseUrl + '/auctions',auction);
  }
  getAuctionById(id:string): Observable<Auction> {
    return this.http.get<Auction>(this.baseUrl + '/auctions/' + id);
  }
  getAuctionDetailsById(id:string): Observable<Auction> {
    return this.http.get<Auction>(this.baseUrl + '/auctions/' + id + 'details');
  }
  //currently returns no content from controller
  updateAuctionById(id:string,auction:Auction): Observable<Auction> {
    return this.http.put<Auction>(this.baseUrl + '/auctions/' + id, auction);
  }
  updateAuctionDetailsById(id:string,auction:Auction): Observable<Auction> {
    return this.http.put<Auction>(this.baseUrl + '/auctions/' + id + 'details',auction);
  }
  //Store
  getStorePacks(): Observable<Pack[]>
  {
    return this.http.get<Pack[]>(`${this.baseUrl}/store`);
  }
  //Order
  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl + '/order');
  }

  getCardById(id:string): Observable<Card[]>{
    return this.http.get<Card[]>(this.baseUrl + '/cards/' + id);
  }

  postOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(this.baseUrl + '/order', order);
  }

  //Card

  //Pack

}