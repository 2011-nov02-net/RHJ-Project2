export interface Order
{
    orderId:string;
    userId:string;
    // use | date in an html element
    date:Date;
    // use | currency in an html element
    total:number;
    packId:string;
    packQty: number;
}