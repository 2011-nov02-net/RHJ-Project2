import { Component, OnInit } from '@angular/core';
import { Auction } from 'src/app/interfaces/auction';
import { Card } from 'src/app/interfaces/card';
import { User } from 'src/app/interfaces/user';
import { ActivatedRoute } from '@angular/router';
import { BackendService } from 'src/app/services/backend.service';

@Component({
  selector: 'app-auction-detail',
  templateUrl: './auction-detail.component.html',
  styleUrls: ['./auction-detail.component.css']
})
export class AuctionDetailComponent implements OnInit {

  auction: Auction | any;
  card: Card | any;
  email: any;
  buyer: User | any;
  offerer: string | any;

  constructor(private backend: BackendService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.email = JSON.parse(localStorage.getItem('email') || '{}');
    this.getAuctionById();
    this.getUser(this.email.email);
    this.getCard();
  }

  getAuctionById(): void {
    let id = this.route.snapshot.params.id;
    this.backend.getAuctionById(id).subscribe(auction => this.auction = auction);
  }

  getUser(email:string): void {
    //this.backend.getUserById(id).subscribe(buyer => this.buyer = buyer);
    this.backend.getUserByEmail(email).subscribe(buyer => this.buyer = buyer);
  }

  getCard(): void {
    let cid = this.route.snapshot.params.cid;
    this.backend.getCardById(cid).subscribe(card => this.card = card);
  }

}
