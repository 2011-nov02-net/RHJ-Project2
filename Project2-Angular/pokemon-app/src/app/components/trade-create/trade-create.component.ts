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

  constructor(private backend: BackendService) { }

  ngOnInit(): void {
    this.getUserInventory();
  }

    //get users specific inventory
    getUserInventory(): void {
      this.backend.getUserCards().subscribe(cards => this.userCards = cards);
    }
  
    onSelectedCard(cardId:string): void{
      this.backend.getCardById(cardId).subscribe(card => this.card = card);
    }

    //add user id to offerer id
    //get last trade id + 1
    createTrade(cardId:string): void {
      let trade: Trade = {
        tradeId: "trade1003",
        OffererId: "cus1",
        BuyerId: "",
        isClosed: false,
        TradeDate: new Date(),
        offerCardId: cardId,
        buyerCardId: ""
      };

      this.backend.postTrade(trade).subscribe();
      console.log("Clicked Create Trade");
    }

}
