import { Component, OnInit } from '@angular/core';
 

import { Card } from 'src/app/interfaces/card';
import { BackendService } from '../../services/backend.service';


@Component({
  selector: 'app-collection',
  templateUrl: './collection.component.html',
  styleUrls: ['./collection.component.css']
})
export class CollectionComponent implements OnInit {

  userId!:string;
  cards: Card[] | any;
  selectedCard: Card | any;

  constructor( private backendService:BackendService) { }

  ngOnInit(): void {
     
    this.userId = JSON.parse(localStorage.getItem('id') || '{}');
    this.backendService.getUserCards(this.userId).subscribe((data) => { this.cards = data; });

}
  onSelect(card:Card):void{
    this.selectedCard = card;
    console.log("submitted")
    console.log(this.selectedCard);
     
  }
 
  

}
