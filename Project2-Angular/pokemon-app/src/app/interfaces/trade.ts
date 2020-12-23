export interface Trade
{
    tradeId:string;
    offererId:string;
    buyerId:string;
    isClosed:boolean;
    // use | date in an html element
    TradeDate:Date;
    offerCardId:string;
    buyerCardId:string;
}