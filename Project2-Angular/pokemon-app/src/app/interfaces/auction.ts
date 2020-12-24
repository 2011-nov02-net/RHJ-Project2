export interface Auction
{
    auctionId:string;
    sellerId:string;
    buyerId:string | undefined;
    cardId:string;
    // use | currency in an html element
    priceSold:number;
    priceListed: number;
    buyoutPrice: number;
    sellType: string;
    numberBids: number;
    // use | date in an html element
    sellDate:Date;
    expDate:Date;
}
    
