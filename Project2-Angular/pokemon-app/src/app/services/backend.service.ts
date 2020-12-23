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
import { OrderItem } from '../interfaces/orderItem'

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

  getUserCardsById(userId:string):Observable<Card[]> 
  {
    // userId hard coded, replce with login
    return this.http.get<Card[]>(this.baseUrl + '/users/' + userId + '/cards');
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
    console.log("Trade");
    return this.http.post<Trade>(this.baseUrl + '/trades',trade);
  }

  getTradeById(id:string): Observable<Trade> {
    return this.http.get<Trade>(this.baseUrl + '/trades/' + id);

  }
  updateTradeById(id:string,trade:Trade): Observable<Trade> {
    return this.http.put<Trade>(this.baseUrl + '/trades/' + id,trade);
  }
  //Auctions
  getAuctions(): Observable<Auction[]> {
    return this.http.get<Auction[]>(this.baseUrl + '/auctions');
  }
  
  postAuction(auction:Auction):Observable<Auction>{
    return this.http.post<Auction>(this.baseUrl + '/auctions',auction);
  }
  getAuctionById(id:string): Observable<Auction> {
    return this.http.get<Auction>(this.baseUrl + '/auctions/' + id);
  }

  updateAuctionById(id:string,auction:Auction): Observable<Auction> {
    return this.http.put<Auction>(this.baseUrl + '/auctions/' + id, auction);
  }
  //Store
  getStorePacks(): Observable<Pack[]>
  {
    return this.http.get<Pack[]>(`${this.baseUrl}/store`);
  }
  getStorePackById(id:string): Observable<Pack>
  {
    return this.http.get<Pack>(`${this.baseUrl}/store/${id}`);
  }
  updateStorePackById(id:string,pack:Pack): Observable<Pack> {
    return this.http.put<Pack>(this.baseUrl + '/store/' + id, pack);
  }
  //Order
  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl + '/order');
  }

  getOrderById(id:string): Observable<Order> {
    return this.http.get<Order>(`${this.baseUrl}/order/${id}`);
  }
  getOrderItemsById(id:string): Observable<OrderItem[]> {
    return this.http.get<OrderItem[]>(`${this.baseUrl}/order/${id}/details`);
  }
  postOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(this.baseUrl + '/order', order);
  }

  //Card
  getCards(): Observable<Card[]>{
    return this.http.get<Card[]>(this.baseUrl + '/cards/');
  }
  getCardById(id:string): Observable<Card>{
    return this.http.get<Card>(this.baseUrl + '/cards/' + id);
  }
  postCard(card: Card): Observable<Card>{
    return this.http.post<Card>(this.baseUrl + '/cards/', card);
  }
  //Pack
  getPacks(): Observable<Pack[]>{
    return this.http.get<Pack[]>(this.baseUrl + '/packs/');
  }
  getPackById(id:string): Observable<Pack>{
    return this.http.get<Pack>(this.baseUrl + '/packs/' + id);
  }
}