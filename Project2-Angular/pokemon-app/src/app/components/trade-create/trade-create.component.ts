import { Component, OnInit } from '@angular/core';
import { Trade } from 'src/app/interfaces/trade';
import { Card } from 'src/app/interfaces/card';
import { BackendService } from 'src/app/services/backend.service';

@Component({
  selector: 'app-trade-create',
  templateUrl: './trade-create.component.html',
  styleUrls: ['./trade-create.component.css']
})
export class TradeCreateComponent implements OnInit {

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

    //add user id to offerer id
    //get last trade id + 1
    createTrade(cardId:string): void {
      let trade: Trade = {
        tradeId: "trade1007",
        offererId: "cus1",
        buyerId: "",
        isClosed: false,
        TradeDate: new Date(),
        offerCardId: cardId,
        buyerCardId: ""
      };

      this.backend.postTrade(trade).subscribe();
      console.log("Clicked Create Trade");
    }

}
