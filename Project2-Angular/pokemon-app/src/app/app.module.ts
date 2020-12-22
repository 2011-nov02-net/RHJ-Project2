import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AuthGuard } from './auth/auth.guard';
 

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './components/user/user.component';
import { AboutComponent } from './components/about/about.component';
import { NavComponent } from './components/nav/nav.component';
import { StoreComponent } from './components/store/store.component';
import { CollectionComponent } from './components/collection/collection.component';
import { PurchaseComponent } from './components/purchase/purchase.component';
import { TradeComponent } from './components/trade/trade.component';
import { AuctionComponent } from './components/auction/auction.component';
import { HistoryComponent } from './components/history/history.component';
import { ConvertRarityPipe } from './pipes/convert-rarity.pipe';

import { PoketcgService} from './services/poketcg.service';
import { BackendService} from './services/backend.service';
import { TradeDetailComponent } from './components/trade-detail/trade-detail.component';
import { TradeCardComponent } from './components/trade-card/trade-card.component';
import { TradeCreateComponent } from './components/trade-create/trade-create.component';



@NgModule({
  declarations: [
    AppComponent,
    ConvertRarityPipe,
    LoginComponent,
    UserComponent,
    CollectionComponent,
    PurchaseComponent,
    TradeComponent,
    AuctionComponent,
    StoreComponent,
    AboutComponent,
    NavComponent,
    HistoryComponent,
    TradeDetailComponent,
    TradeCardComponent,
    TradeCreateComponent, 
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,      
  ],
  providers: [PoketcgService,BackendService,AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }