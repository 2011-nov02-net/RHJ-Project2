-- insert customers
-- have not purchased any pack

insert into TEAM.Customer(UserId,First,Last,Email,UserRole,NumPacksPurchased,CurrencyAmount) values('1','Ryan','Towner','RY@gmail.com','Regular',0,10);
insert into TEAM.Customer(UserId,First,Last,Email,UserRole,NumPacksPurchased,CurrencyAmount) values('2','Joseph','Ellis','JE@gmail.com','Regular',0,2);
insert into TEAM.Customer(UserId,First,Last,Email,UserRole,NumPacksPurchased,CurrencyAmount) values('3','Hao','Yang','HY@gmail.com','Regular',1,3);

insert into TEAM.Customer(UserId,First,Last,Email,UserRole,NumPacksPurchased,CurrencyAmount) values('cus4','Test','Test','TTgmail.com','Regular',5,500);

-- I am out of cash
update TEAM.Customer set CurrencyAmount = 500 where UserId= '1';
update TEAM.Customer set CurrencyAmount = 500 where UserId= '2';
update TEAM.Customer set CurrencyAmount = 500 where UserId= '3';

-- insert pack
-- dummies for testing
insert into TEAM.Pack(PackId,Name,Price) values('1','Legend Origin', 10);
insert into TEAM.Pack(PackId,Name,Price) values('2','Legend Hyper', 11);
insert into TEAM.Pack(PackId,Name,Price) values('3','Bug Out', 12);

-- inesrt card
-- dummies for testing, will use pokeAPI to get data
insert into TEAM.Card(CardId,Name,Type,Rarity,Value,Rating,NumOfRatings,Image) values('card101','Mew','Psychic',5,8,1,1,'https://images.pokemontcg.io/xy10/29.png');
insert into TEAM.Card(CardId,Name,Type,Rarity,Value,Rating,NumOfRatings,Image) values('card102','MewTwo','Psychic',6,8,3,5,'https://images.pokemontcg.io/dp6/11.png');
insert into TEAM.Card(CardId,Name,Type,Rarity,Value,Rating,NumOfRatings,Image) values('card103','Pinsir','Bug',4,5,1,2,'https://images.pokemontcg.io/dp3/59.png');
insert into TEAM.Card(CardId,Name,Type,Rarity,Value,Rating,NumOfRatings,Image) values('base1-28','Growlithe','Fire',4,5,1,2,'https://images.pokemontcg.io/base1/28.png');
insert into TEAM.Card(CardId,Name,Type,Rarity,Value,Rating,NumOfRatings,Image) values('base1-29','Haunter','Psychic',4,5,1,2,'https://images.pokemontcg.io/ex6/34.png');


-- insert user card inventory
-- each auctioned one card
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('1','card101',1);
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('2','card102',1);
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('2','card103',1);
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('2','base1-28',1);
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('3','card103',1);

-- cards from one order of one pack
insert into TEAM.UserCardInventory(UserId, CardId, Quantity) values('3','card103',8);


-- insert store inventory
insert into TEAM.StoreInventory(PackId,PackQty) values('1',100);
insert into TEAM.StoreInventory(PackId,PackQty) values('2',50);
insert into TEAM.StoreInventory(PackId,PackQty) values('3',300);

-- insert auction
insert into TEAM.Auction(AuctionId,SellerId,BuyerId,CardId,PriceSold) values('Auc1001','2','1','card101',20);
insert into TEAM.Auction(AuctionId,SellerId,BuyerId,CardId,PriceSold) values('Auc1002','3','2','card102',15);
insert into TEAM.Auction(AuctionId,SellerId,BuyerId,CardId,PriceSold) values('Auc1003','1','3','card103',10);


-- insert auction detail
insert into TEAM.AuctionDetail(AuctionId,PriceListed,BuyoutPrice,NumberBids,SellType,expDate) values('Auc1001', 5, 50, 10, 'Bid','2021-2-1');
insert into TEAM.AuctionDetail(AuctionId,PriceListed,BuyoutPrice,NumberBids,SellType,expDate) values('Auc1002', 6, 60, 10, 'Bid','2021-2-1');
insert into TEAM.AuctionDetail(AuctionId,PriceListed,BuyoutPrice,NumberBids,SellType,expDate) values('Auc1003', 1, 10, 4, 'Buyout','2021-2-1');

-- insert order
-- insert orderitem
insert into TEAM.[Order](OrderId,UserId,Total) values('order777','3',12);
insert into TEAM.OrderItem(OrderId,PackId,PackQty) values('order777','PackC',1);


-- update store inventory
update TEAM.StoreInventory set PackQty = 299 where PackId= '3';

-- insert trade
insert into TEAM.Trade(TradeId, OffererId, BuyerId) values('trade1001', '1', '2');
insert into TEAM.Trade(TradeId, OffererId, BuyerId) values('trade1002', '1', '2');
-- insert trade detail
insert into TEAM.TradeDetail(TradeId, OfferCardId, BuyerCardId) values('trade1001', 'card101', 'card102');
insert into TEAM.TradeDetail(TradeId, OfferCardId, BuyerCardId) values('trade1002', 'base1-28', 'base1-29');
-- no trades made yet, not enough cards atm

DELETE FROM TEAM.Auction WHERE AuctionId = '2';
DELETE FROM TEAM.AuctionDetail WHERE AuctionId = '2';

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