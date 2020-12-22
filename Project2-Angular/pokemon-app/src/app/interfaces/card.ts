export interface Card
{
    CardId:string;
    image:string;
    Name:string;
    Type:string;
    // make a custom pipe, common-> 1, uncommon-> 2, rare->3
    Rarity:number;
    // use | currency in an html element
    Value:number;
    Img:string;
}