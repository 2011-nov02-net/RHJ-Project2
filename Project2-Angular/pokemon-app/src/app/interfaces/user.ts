export interface User
{
    UserId:string;
    Frist:string;
    Last:string;
    Email:string;
    // extras can be removed
    UserRole:string;
    NumberPacksPurchased:number;
    // use | currency in an html element
    CurrencyAmount:number;
}