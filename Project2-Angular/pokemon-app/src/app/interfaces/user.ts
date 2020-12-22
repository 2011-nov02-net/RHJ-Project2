export interface User
{
    userId:string;
    first:string;
    last:string;
    email:string;
    // extras can be removed
    //userRole:string;
    numPacksPurchased:number;
    // use | currency in an html element
    currencyAmount:number;
}