export interface Trade
{
    tradeId:string;
    OffererId:string;
    BuyerId:string;
    isClosed:boolean;
    // use | date in an html element
    TradeDate:Date;
    offerCardId:string;
    buyerCardId:string;
}