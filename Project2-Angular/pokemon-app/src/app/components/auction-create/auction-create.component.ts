import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Auction } from 'src/app/interfaces/auction';
import { Card } from 'src/app/interfaces/card';
import { BackendService } from 'src/app/services/backend.service';

@Component({
  selector: 'app-auction-create',
  templateUrl: './auction-create.component.html',
  styleUrls: ['./auction-create.component.css']
})
export class AuctionCreateComponent implements OnInit {

  userCards: Card[] | any;
  userId: any;
  
  card: Card | any;
  listPrice: number = 0;
  buyoutPrice: number = 0;
  sellType: string = "Bid";
  expDate: Date | any;

  constructor(private backend: BackendService) { }

  ngOnInit(): void {
    this.userId = JSON.parse(localStorage.getItem('id') || '{}');
    this.getUserInventory(this.userId.id);
    this.buyoutPrice = 0;
  }

  //get users specific inventory
  getUserInventory(userId:string): void {
    this.backend.getUserCardsById(userId).subscribe(cards => this.userCards = cards);
  }

  onSelectedCard(cardId:string): void{
    this.backend.getCardById(cardId).subscribe(card => this.card = card);
  }

  onSubmit(event: any) {
    this.listPrice = event.target.listInput.value;
    if(event.target.buyoutInput.value != "") {
      this.buyoutPrice = event.target.buyoutInput.value;
      this.sellType = 'Buyout';
    }
    else {
      this.buyoutPrice = 0;
      this.sellType = 'Bid';
    }
    this.expDate = event.target.expInput.value;

    this.postAuction();
  }

  postAuction(): void {
    let auction: Auction = {
      auctionId: '2',
      sellerId: this.userId.id,
      buyerId: '',
      cardId: this.card.cardId,
      priceListed: this.listPrice,
      buyoutPrice: this.buyoutPrice,
      sellType: this.sellType,
      priceSold: 0,
      numberBids: 0,
      sellDate: this.expDate,
      expDate: this.expDate
    }

    this.backend.postAuction(auction).subscribe();
  }
}
