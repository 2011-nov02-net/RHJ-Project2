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
import { TradeComponent } from './components/trade/trade.component';
import { TradeDetailComponent } from './components/trade-detail/trade-detail.component';
import { TradeCreateComponent } from './components/trade-create/trade-create.component';
import { HistoryComponent } from './components/history/history.component';

// all routes handled here
const routes: Routes = [{ path:'',component:LoginComponent},
{path:'user',component:UserComponent},
{path:'user/:id', component: UserComponent },
{path:'collection',component:CollectionComponent},
{path:'purchase',component:PurchaseComponent},
{path:'auctions',component:AuctionComponent},
{path:'trades',component:TradeComponent},
{path:'trades/:id/:cid',component:TradeDetailComponent},
{path:'trades/create',component:TradeCreateComponent},
{path:'history/auctions',component:HistoryComponent},
{path:'history/trades',component:HistoryComponent},

// guarded
{path:'store',component:StoreComponent/*,canActivate:[AuthGuard]*/},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }