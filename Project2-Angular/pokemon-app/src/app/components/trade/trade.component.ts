import { Component, OnInit} from '@angular/core';
import { Trade } from 'src/app/interfaces/trade';
import { Card } from 'src/app/interfaces/card';
import { BackendService } from 'src/app/services/backend.service';

@Component({
  selector: 'app-trade',
  templateUrl: './trade.component.html',
  styleUrls: ['./trade.component.css']
})
export class TradeComponent implements OnInit {

  trades: Trade[] = [];
  cards: Card[] = [];

  constructor(private backend: BackendService) { }

  ngOnInit(): void {
    this.getTrades();
    //this.getCard('base1-28');
  }
  
  getTrades(): void {
    this.backend.getTrades().subscribe(trades => this.trades = trades);
  }

}
