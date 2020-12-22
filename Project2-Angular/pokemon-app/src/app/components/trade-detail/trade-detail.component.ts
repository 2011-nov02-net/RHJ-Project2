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


  constructor(private backend: BackendService, private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.getTradeById();
    this.getOfferCard();
    this.getUserInventory();
  }

  getTradeById(): void {
    let id = this.route.snapshot.params.id;
    this.backend.getTradeById(id).subscribe(trade => this.trade = trade);
  }

  getOfferCard(): void {
    let id = this.route.snapshot.params.cid;
    this.backend.getCardById(id).subscribe(card => this.offerCard = card);
  }

  //get users specific inventory
  getUserInventory(): void {
    this.backend.getUserCards().subscribe(cards => this.userCards = cards);
  }

  onSelectedCard(cardId:string): void{
    this.backend.getCardById(cardId).subscribe(card => this.buyerCard = card);
  }
}
