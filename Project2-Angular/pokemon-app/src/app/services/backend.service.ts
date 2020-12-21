import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// import dev environment
import { environment} from '../../environments/environment';
// need to be added
import { environmentProd} from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class BackendService {
  // to be replaced by app service url
  private baseUrl =  'https://localhost:44301/api';
  // private baseUrl = ` ${environment.baseUrl}`;

  constructor(private http:HttpClient) { }
  
  // promise -> observable  
  getUserCards() 
  {
    // userId hard coded, replce with login
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
