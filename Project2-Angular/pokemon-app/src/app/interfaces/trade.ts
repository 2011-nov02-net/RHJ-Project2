export interface Trade
{
    TradeId:string;
    OffererId:string;
    BuyerId:string;
    IsClosed:boolean;
    // use | date in an html element
    TradeDate:Date;
}