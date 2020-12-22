export interface Card
{
    CardId:string;
    Image: string;
    Name:string;
    Type:string;
    // make a custom pipe, common-> 1, uncommon-> 2, rare->3
    Rarity:number;
    // use | currency in an html element
    Value:number;
<<<<<<< HEAD
    Img:string;
=======
    Rating:number;
    NumberOfRatings:number;
    
>>>>>>> 1995b6357dd9e8bd2d71dbbe259ac4ed81948b46
}