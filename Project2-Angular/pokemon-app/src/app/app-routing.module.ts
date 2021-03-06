import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
 

import { LoginComponent } from './login/login.component';
import { UserComponent } from './components/user/user.component';
import { CollectionComponent } from './components/collection/collection.component';
import { PurchaseComponent } from './components/purchase/purchase.component';
import { StoreComponent } from './components/store/store.component';
import { AuthGuard } from './auth/auth.guard';
import { NavComponent } from './components/nav/nav.component';
import { AuctionComponent } from './components/auction/auction.component';
import { AuctionCreateComponent } from './components/auction-create/auction-create.component';
import { AuctionDetailComponent } from './components/auction-detail/auction-detail.component';
import { TradeComponent } from './components/trade/trade.component';
import { TradeDetailComponent } from './components/trade-detail/trade-detail.component';
import { TradeCreateComponent } from './components/trade-create/trade-create.component';
import { HistoryComponent } from './components/history/history.component';
import { AboutComponent } from './components/about/about.component';

// all routes handled here
const routes: Routes = [{ path:'',component:LoginComponent},
{path:'user',component:UserComponent},
{path:'user/:id', component: UserComponent },
{path:'collection',component:CollectionComponent},
{path:'store',component:StoreComponent},
{path:'auctions',component:AuctionComponent},
{path:'auctions/create',component:AuctionCreateComponent},
{path:'auctions/:id/:cid',component:AuctionDetailComponent},
{path:'trades',component:TradeComponent},
{path:'trades/:id/:cid',component:TradeDetailComponent},
{path:'trades/create',component:TradeCreateComponent},
{path:'history/auctions',component:HistoryComponent},
{path:'history/trades',component:HistoryComponent},
{path:'about',component:AboutComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }