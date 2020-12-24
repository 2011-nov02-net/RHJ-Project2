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
  amount: number = 0;

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

  bidChange(num:any): void {
    this.amount = num.target.value;
  }

  placeBid(): void {
    let user: User = {
      userId: this.buyer.userId,
      first: this.buyer.first,
      last: this.buyer.last,
      email: this.buyer.email,
      numPacksPurchased: this.buyer.numPacksPurchased,
      currencyAmount: this.buyer.currencyAmount - this.amount,
      userRole: "1"
    }

    let increase:number = this.amount - this.auction.priceListed;

    let auction: Auction = {
      auctionId: this.auction.auctionId,
      sellerId: this.auction.sellerId,
      buyerId: this.buyer.userId,
      cardId: this.auction.cardId,
      priceListed: this.auction.priceListed + increase,
      buyoutPrice: this.auction.buyoutPrice,
      sellType: "Bid",
      priceSold: this.auction.priceSold,
      numberBids: this.auction.numberBids,
      sellDate: this.auction.sellDate,
      expDate: this.auction.expDate
    }
    //update user currency and auction bid amount
    this.backend.updateUserById(this.buyer.userId, user).subscribe();
    this.backend.updateAuctionById(this.auction.auctionId, auction).subscribe();
    alert("Placed Bid Successfully!");

  }

  buyNow(): void {
    let user: User = {
      userId: this.buyer.userId,
      first: this.buyer.first,
      last: this.buyer.last,
      email: this.buyer.email,
      numPacksPurchased: this.buyer.numPacksPurchased,
      currencyAmount: this.buyer.currencyAmount - this.auction.buyoutPrice,
      userRole: "1"
    }

    let auction: Auction = {
      auctionId: this.auction.auctionId,
      sellerId: this.auction.sellerId,
      buyerId: this.buyer.userId,
      cardId: this.auction.cardId,
      priceListed: this.auction.buyoutPrice,
      buyoutPrice: this.auction.buyoutPrice,
      sellType: this.auction.sellType,
      priceSold: this.auction.buyoutPrice,
      numberBids: this.auction.numberBids,
      sellDate: new Date(),
      expDate: this.auction.expDate
    }
    //update user currency and auction bid amount
    this.backend.updateUserById(this.buyer.userId, user).subscribe();
    this.backend.updateAuctionById(this.auction.auctionId, auction).subscribe();
    alert("Bought Out Auction Successfully!");
  }

}
