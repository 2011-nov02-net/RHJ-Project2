import { Component, OnInit } from '@angular/core';
import { Card } from 'src/app/interfaces/card';
import { Auction } from 'src/app/interfaces/auction';
import { BackendService } from 'src/app/services/backend.service';

@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.css']
})
export class AuctionComponent implements OnInit {

  auctions: Auction[] = [];
  cards: Card[] = [];

  //cardId="{{a.CardId}}" auctionId="{{a.AuctionId}}"

  constructor(private backend: BackendService) { }

  ngOnInit(): void {
    this.getAuctions();
  }

  getAuctions(): void {
    this.backend.getAuctions().subscribe(auctions => this.auctions = auctions);
  }

}
