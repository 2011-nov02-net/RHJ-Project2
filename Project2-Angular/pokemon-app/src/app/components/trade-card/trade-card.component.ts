import { Component, OnInit, Input } from '@angular/core';
import { Trade } from 'src/app/interfaces/trade';
import { Card } from 'src/app/interfaces/card';
import { BackendService } from 'src/app/services/backend.service';

@Component({
  selector: 'app-trade-card',
  templateUrl: './trade-card.component.html',
  styleUrls: ['./trade-card.component.css']
})
export class TradeCardComponent implements OnInit {
  @Input() tradeId:string | any;
  @Input() cardId:string | any;

  card: Card | any;
  trade: Trade | any;

  constructor(private backend: BackendService) { }

  ngOnInit(): void {
    this.getCard(this.cardId);
    this.getTrade(this.tradeId);
  }

  getCard(cardId:string): void {
    this.backend.getCardById(cardId).subscribe(card => this.card = card)
  }

  getTrade(tradeId:string): void {
    this.backend.getTradeById(tradeId).subscribe(trades => this.trade = trades);
  }

}
