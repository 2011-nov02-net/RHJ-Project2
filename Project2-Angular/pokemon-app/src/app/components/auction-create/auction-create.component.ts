import { Component, OnInit } from '@angular/core';
import { Auction } from 'src/app/interfaces/auction';
import { Card } from 'src/app/interfaces/card';
import { BackendService } from 'src/app/services/backend.service';

@Component({
  selector: 'app-auction-create',
  templateUrl: './auction-create.component.html',
  styleUrls: ['./auction-create.component.css']
})
export class AuctionCreateComponent implements OnInit {

  userCards: Card[] | any;
  card: Card | any;
  userId: any;

  constructor(private backend: BackendService) { }

  ngOnInit(): void {
    this.userId = JSON.parse(localStorage.getItem('id') || '{}');
    this.getUserInventory(this.userId.id);
  }

  //get users specific inventory
  getUserInventory(userId:string): void {
    this.backend.getUserCardsById(userId).subscribe(cards => this.userCards = cards);
  }

  onSelectedCard(cardId:string): void{
    this.backend.getCardById(cardId).subscribe(card => this.card = card);
  }

}
