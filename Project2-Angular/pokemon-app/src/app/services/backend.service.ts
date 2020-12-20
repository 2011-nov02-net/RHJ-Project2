import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Card } from '../interfaces/card';

@Injectable({
  providedIn: 'root'
})
export class BackendService {
  // to be replaced by app service url
  private baseUrl = 'https://localhost:44301/api'

  constructor(private http:HttpClient) { }
  
  // promise -> observable
  
  getUserCards()//Promise<Card[]>
  {
    // how to replace userId
    // <Card[]>
    return this.http.get(`${this.baseUrl}/users/cus2/cards`);
  }
  
 /*
 getUserCards():Promise<Card[]>
 {
   // how to replace userId
   // <Card[]>
   return this.http.get<Card[]>(`${this.baseUrl}/users`).toPromise();
 }
 */

}
