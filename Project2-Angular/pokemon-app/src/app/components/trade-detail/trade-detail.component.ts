import { Component, OnInit } from '@angular/core';
import { Trade } from 'src/app/interfaces/trade';
import { Card } from 'src/app/interfaces/card';
import { ActivatedRoute } from '@angular/router';
import { BackendService } from 'src/app/services/backend.service';

@Component({
  selector: 'app-trade-detail',
  templateUrl: './trade-detail.component.html',
  styleUrls: ['./trade-detail.component.css']
})
export class TradeDetailComponent implements OnInit {

  trade: Trade | any;
  offerCard: Card | any;
  buyerCard: Card | any;
  userCards: Card[] | any;
  userId: any;
  buyerCardId: string | any;
  buyer: string | any;
  offerer: string | any;


  constructor(private backend: BackendService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.userId = JSON.parse(localStorage.getItem('id') || '{}');
    this.getTradeById();
    this.getOfferCard();
    this.getUserInventory(this.userId.id);
  }

  getTradeById(): void {
    let id = this.route.snapshot.params.id;
    this.backend.getTradeById(id).subscribe(trade => {
      this.trade = trade;
      this.buyer = trade.buyerId;
      this.buyerCardId = trade.buyerCardId;
      this.onSelectedCard(this.buyerCardId);
    });
  }

  getOfferCard(): void {
    let id = this.route.snapshot.params.cid;
    this.backend.getCardById(id).subscribe(card => this.offerCard = card);
  }

  //get users specific inventory
  getUserInventory(userId:string): void {
    this.backend.getUserCardsById(userId).subscribe(cards => this.userCards = cards);
  }

  onSelectedCard(cardId:string): void{
    this.backend.getCardById(cardId).subscribe(card => this.buyerCard = card);
  }

  //update trade
  updateTrade(): void {
    let upTrade: Trade = {
      tradeId: this.trade.tradeId,
      offererId: this.trade.offererId,
      buyerId: this.userId.id,
      isClosed: false,
      TradeDate: this.trade.TradeDate,
      offerCardId: this.offerCard.cardId,
      buyerCardId: this.buyerCard.cardId
    }

    this.backend.updateTradeById(this.trade.tradeId, upTrade).subscribe();
    alert("Trade Offer Placed");
  }

  approveTrade(): void {
    //update trade to closed
    let upTrade: Trade = {
      tradeId: this.trade.tradeId,
      offererId: this.trade.offererId,
      buyerId: this.buyer,
      isClosed: true,
      TradeDate: this.trade.TradeDate,
      offerCardId: this.offerCard.cardId,
      buyerCardId: this.buyerCard.cardId
    }

    this.backend.updateTradeById(this.trade.tradeId, upTrade).subscribe();
    alert("Trade Approved");
  }
}
