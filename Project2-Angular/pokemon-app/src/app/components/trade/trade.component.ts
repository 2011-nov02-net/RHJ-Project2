import { Component, OnInit } from '@angular/core';
import { Trade } from 'src/app/interfaces/trade';
import { BackendService } from 'src/app/services/backend.service';

@Component({
  selector: 'app-trade',
  templateUrl: './trade.component.html',
  styleUrls: ['./trade.component.css']
})
export class TradeComponent implements OnInit {

  trades: Trade[] | any;
  tradeId:string = 'trade1001';

  constructor(private backend: BackendService) { }

  ngOnInit(): void {
    this.getTrades();
  }

  getTrades(): void {
    this.backend.getTrades().subscribe(trades => this.trades = trades);
  }

}
