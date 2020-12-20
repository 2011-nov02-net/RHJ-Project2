import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PoketcgService {
  // url to be changed
  url:string = 'https://api.pokemontcg.io/v1/sets'
  constructor(public http: HttpClient) {}

  getPoketcg(){
    return this.http.get(this.url);
  }
}
