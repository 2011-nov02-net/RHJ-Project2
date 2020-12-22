-- insert customers
-- have not purchased any pack
insert into TEAM.Customer(UserId,First,Last,Email,UserRole,NumPacksPurchased,CurrencyAmount) values('cus1','Ryan','Towner','RY@gmail.com','Regular',0,10);
insert into TEAM.Customer(UserId,First,Last,Email,UserRole,NumPacksPurchased,CurrencyAmount) values('cus2','Joseph','Ellis','JE@gmail.com','Regular',0,2);
insert into TEAM.Customer(UserId,First,Last,Email,UserRole,NumPacksPurchased,CurrencyAmount) values('cus3','Hao','Yang','HY@gmail.com','Regular',1,3);

insert into TEAM.Customer(UserId,First,Last,Email,UserRole,NumPacksPurchased,CurrencyAmount) values('cus4','Test','Test','TTgmail.com','Regular',5,5);

-- insert pack
-- dummies for testing
insert into TEAM.Pack(PackId,Name,Price) values('packA','Legend Origin', 10);
insert into TEAM.Pack(PackId,Name,Price) values('packB','Legend Hyper', 11);
insert into TEAM.Pack(PackId,Name,Price) values('packC','Bug Out', 12);

-- inesrt card
-- dummies for testing, will use pokeAPI to get data
insert into TEAM.Card(CardId,Name,Type,Rarity,Value,Rating,NumOfRatings,Image) values('card101','Mew','Psychic',5,8,1,1,'https://images.pokemontcg.io/xy10/29.png');
insert into TEAM.Card(CardId,Name,Type,Rarity,Value,Rating,NumOfRatings,Image) values('card102','MewTwo','Psychic',6,8,3,5,'https://images.pokemontcg.io/dp6/11.png');
insert into TEAM.Card(CardId,Name,Type,Rarity,Value,Rating,NumOfRatings,Image) values('card103','Pinsir','Bug',4,5,1,2,'https://images.pokemontcg.io/dp3/59.png');
insert into TEAM.Card(CardId,Name,Type,Rarity,Value,Rating,NumOfRatings,Image) values('base1-28','Growlithe','Fire',4,5,1,2,'https://images.pokemontcg.io/base1/28.png');
insert into TEAM.Card(CardId,Name,Type,Rarity,Value,Rating,NumOfRatings,Image) values('base1-29','Haunter','Psychic',4,5,1,2,'https://images.pokemontcg.io/ex6/34.png');


-- insert user card inventory
-- each auctioned one card
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('cus1','card101',1);
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('cus2','card102',1);
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('cus2','card103',1);
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('cus2','base1-28',1);
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('cus3','card103',1);

-- cards from one order of one pack
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('cus3','card103',8);


-- insert store inventory
insert into TEAM.StoreInventory(PackId,PackQty) values('packA',100);
insert into TEAM.StoreInventory(PackId,PackQty) values('packB',50);
insert into TEAM.StoreInventory(PackId,PackQty) values('packC',300);

-- insert auction
insert into TEAM.Auction(AuctionId,SellerId,BuyerId,CardId,PriceSold) values('Auc1001','cus2','cus1','card101',20);
insert into TEAM.Auction(AuctionId,SellerId,BuyerId,CardId,PriceSold) values('Auc1002','cus3','cus2','card102',15);
insert into TEAM.Auction(AuctionId,SellerId,BuyerId,CardId,PriceSold) values('Auc1003','cus1','cus3','card103',10);

-- insert auction detail
insert into TEAM.AuctionDetail(AuctionId,PriceListed,BuyoutPrice,NumberBids,SellType) values('Auc1001', 5, 50, 10, 'Bid');
insert into TEAM.AuctionDetail(AuctionId,PriceListed,BuyoutPrice,NumberBids,SellType) values('Auc1002', 6, 60, 10, 'Bid');
insert into TEAM.AuctionDetail(AuctionId,PriceListed,BuyoutPrice,NumberBids,SellType) values('Auc1003', 1, 10, 4, 'Buyout');

-- insert order
-- insert orderitem
insert into TEAM.[Order](OrderId,UserId,Total) values('order777','cus3',12);
insert into TEAM.OrderItem(OrderId,PackId,PackQty) values('order777','PackC',1);

-- update store inventory
update TEAM.StoreInventory set PackQty = 299 where PackId= 'PackC';

-- insert trade
insert into TEAM.Trade(TradeId, OffererId, BuyerId) values('trade1001', 'cus1', 'cus2');
insert into TEAM.Trade(TradeId, OffererId, BuyerId) values('trade1002', 'cus1', 'cus2');
-- insert trade detail
insert into TEAM.TradeDetail(TradeId, OfferCardId, BuyerCardId) values('trade1001', 'card101', 'card102');
insert into TEAM.TradeDetail(TradeId, OfferCardId, BuyerCardId) values('trade1002', 'base1-28', 'base1-29');
-- no trades made yet, not enough cards atm

select * from TEAM.Customer;
select * from TEAM.UserCardInventory;
select * from TEAM.Card;
select * from TEAM.Pack;
select * from TEAM.StoreInventory;
select * from TEAM.Auction;
select * from TEAM.AuctionDetail;
select * from TEAM.[Order];
select * from TEAM.OrderItem;
select * from TEAM.Trade;
select * from TEAM.TradeDetail;
