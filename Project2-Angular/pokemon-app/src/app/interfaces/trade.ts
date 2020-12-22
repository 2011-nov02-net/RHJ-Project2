export interface Trade
{
    TradeId:string;
    OffererId:string;
    BuyerId:string;
    isClosed:boolean;
    // use | date in an html element
    TradeDate:Date;
}