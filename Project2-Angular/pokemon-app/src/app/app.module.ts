import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { ConvertRarityPipe } from './pipes/convert-rarity.pipe';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './components/user/user.component';
import { CollectionComponent } from './components/collection/collection.component';
import { PurchaseComponent } from './components/purchase/purchase.component';
import { TradeComponent } from './components/trade/trade.component';
import { AuctionComponent } from './components/auction/auction.component';

import { PoketcgService} from './services/poketcg.service';
import { BackendService} from './services/backend.service';


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
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    
  ],
  providers: [PoketcgService,BackendService],
  bootstrap: [AppComponent]
})
export class AppModule { }
