import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BackendService } from 'src/app/services/backend.service';
import { Trade } from 'src/app/interfaces/trade';
import { Auction } from 'src/app/interfaces/auction';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  trades: Trade[] | any;
  auctions: Auction[] | any;

  tradehead = ['tradeId','offererId', 'buyerId', 'tradeDate', 'isClosed'];
  aucthead = ['auctionId','buyerId', 'sellerId', 'cardId', 'priceSold', 'sellDate'];

  constructor(
    public _router: Router,
    private backend: BackendService) { }

  ngOnInit(): void {
    this.getTrades();
    this.getAuctions();
  }

  //change to get users trades
  getTrades(): void {
    this.backend.getTrades().subscribe(trades => this.trades = trades);
  }

  //change to get users auctions
  getAuctions(): void {
    this.backend.getAuctions().subscribe(auctions => this.auctions = auctions);
  }

}
