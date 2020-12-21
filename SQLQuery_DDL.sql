Create SCHEMA TEAM;
GO

CREATE TABLE TEAM.Customer(
  UserId nvarchar(40) primary key not null,
  First nvarchar(40) not null,
  Last nvarchar(40) not null,
  Email nvarchar(40) unique not null,
  UserRole nvarchar(40) not null,
  NumPacksPurchased Int not null,
  CurrencyAmount float not null,
  check(NumPacksPurchased >= 0),
  check(CurrencyAmount >= 0)
)

CREATE TABLE TEAM.Pack (
  PackId nvarchar(40) primary key not null,
  Name nvarchar(40) not null,
  Price float not null,
  DateReleased Datetime not null default getdate(),
  check(Price > 0)
)

CREATE TABLE TEAM.Card (
  CardId nvarchar(40) primary key not null,
  Name nvarchar(40) not null,
  Type nvarchar(40) not null,
  Rarity Int not null,
  Value float not null,
  Rating float not null,
  NumOfRatings Int not null,
  check(Rarity >0),
  check(Value >0), 
  check(Rating >0),
)

CREATE TABLE TEAM.UserCardInventory (
  UserCardInvNum int not null PRIMARY KEY IDENTITY,
  UserId nvarchar(40) not null,
  CardId nvarchar(40) not null,
  Quantity int not null,
  check(Quantity >= 0),
  foreign key(UserId) REFERENCES TEAM.Customer(UserId)
      on delete cascade on update cascade,
  foreign key(CardId) REFERENCES TEAM.Card(CardId)
      on delete cascade on update cascade, 
)


CREATE TABLE TEAM.StoreInventory(
  InvNum int not null PRIMARY KEY IDENTITY,
  PackId nvarchar(40) not null,
  PackQty Int not null,
  check(PackQty >= 0),
  foreign key(PackId) REFERENCES TEAM.Pack(PackId)
    on delete cascade on update cascade
)

CREATE TABLE TEAM.Auction(
  AuctionId nvarchar(40) primary key not null,
  SellerId nvarchar(40) not null,
  BuyerId nvarchar(40) null,
  CardId nvarchar(40) not null,
  PriceSold float null,
  SellDate Datetime null default getdate(),
  check(PriceSold > 0),
  foreign key(SellerId) REFERENCES TEAM.Customer(UserId)
    on delete no action on update no action,
  foreign key(BuyerId) REFERENCES TEAM.Customer(UserId)
    on delete no action on update no action,
  foreign key(CardId) REFERENCES TEAM.Card(CardId)
    on delete cascade on update cascade  
)


CREATE TABLE TEAM.AuctionDetail(
  AuctionId nvarchar(40) primary key not null,
  PriceListed float not null,
  BuyoutPrice float not null,
  NumberBids Int,
  SellType nvarchar(40) null,
  ExpDate Datetime not null default getdate(),
  check(PriceListed > 0),
  check(BuyoutPrice > 0),
  check(NumberBids >= 0),
  foreign key(AuctionId) REFERENCES TEAM.Auction(AuctionId)
    on delete cascade on update cascade
)

CREATE TABLE TEAM.[Order](
  OrderId nvarchar(40) primary key not null,
  UserId nvarchar(40) not null,
  Date Datetime not null default getdate(),
  Total float not null,
  check(Total > 0),
  foreign key(UserId) REFERENCES TEAM.Customer(UserId)
    on delete cascade on update cascade
)

CREATE TABLE TEAM.OrderItem(
  OrderItemNum int not null PRIMARY KEY IDENTITY,
  OrderId nvarchar(40) not null,
  PackId nvarchar(40) not null,
  PackQty Int not null,
  check(PackQty > 0),
  foreign key(OrderId) REFERENCES TEAM.[Order](OrderId)
    on delete cascade on update cascade,
  foreign key(PackId) REFERENCES TEAM.Pack(PackId)
    on delete cascade on update cascade
)

CREATE TABLE TEAM.Trade(
  TradeId nvarchar(40) primary key not null,
  OffererId nvarchar(40) not null,
  BuyerId nvarchar(40) null,
  IsClosed bit not null default 0,
  TradeDate Datetime not null default getdate(),
  foreign key(OffererId) REFERENCES TEAM.Customer(UserId)
    on delete no action on update no action,
  foreign key(BuyerId) REFERENCES TEAM.Customer(UserId)
    on delete no action on update no action,
)

CREATE TABLE TEAM.TradeDetail(
  TradeId nvarchar(40) primary key not null,
  OfferCardId nvarchar(40) not null,
  BuyerCardId nvarchar(40) null,
  foreign key(TradeId) REFERENCES TEAM.Trade(TradeId)
    on delete no action on update no action,
  foreign key(OfferCardId) REFERENCES TEAM.Card(CardId)
    on delete no action on update no action,  
  foreign key(BuyerCardId) REFERENCES TEAM.Card(CardId)
    on delete no action on update no action,  
)


drop table TEAM.TradeDetail; drop table TEAM.Trade; drop table TEAM.OrderItem;
drop table TEAM.[Order]; drop table TEAM.AuctionDetail; drop table TEAM.Auction; drop table TEAM.StoreInventory;
drop table TEAM.UserCardInventory; drop table TEAM.Card; drop table TEAM.Pack; drop table TEAM.Customer


