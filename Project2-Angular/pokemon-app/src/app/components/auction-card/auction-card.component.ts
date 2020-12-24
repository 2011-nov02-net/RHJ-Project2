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

  constructor() { }

  ngOnInit(): void {
  }

}
