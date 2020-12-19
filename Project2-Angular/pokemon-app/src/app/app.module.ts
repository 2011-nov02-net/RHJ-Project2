import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { ConvertRarityPipe } from './pipes/convert-rarity.pipe';
import { LoginComponent } from './login/login.component';
import { PoketcgService} from './services/poketcg.service';
import { UserComponent } from './components/user/user.component';


@NgModule({
  declarations: [
    AppComponent,
    ConvertRarityPipe,
    LoginComponent,
    UserComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [PoketcgService],
  bootstrap: [AppComponent]
})
export class AppModule { }
