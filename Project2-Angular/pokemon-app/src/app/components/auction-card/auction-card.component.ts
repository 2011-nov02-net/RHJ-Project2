import { Component, OnInit, Input } from '@angular/core';
import { Auction } from 'src/app/interfaces/auction';
import { Card } from 'src/app/interfaces/card';
import { BackendService } from 'src/app/services/backend.service';

@Component({
  selector: 'app-auction-card',
  templateUrl: './auction-card.component.html',
  styleUrls: ['./auction-card.component.css']
})
export class AuctionCardComponent implements OnInit {

  @Input() auctionId:string | any;
  @Input() cardId:string | any;

  card: Card | any;
  auction: Auction | any;

  constructor(private backend: BackendService) { }

  ngOnInit(): void {
    this.getCard(this.cardId);
    this.getAuction(this.auctionId);
  }

  getCard(cardId:string): void {
    this.backend.getCardById(cardId).subscribe(card => this.card = card)
  }

  getAuction(auctionId:string) {
    this.backend.getAuctionById(auctionId).subscribe(auction => this.auction = auction)
  }

}
