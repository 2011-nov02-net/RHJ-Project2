import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { UserComponent } from './components/user/user.component';
import { CollectionComponent } from './components/collection/collection.component';
import { PurchaseComponent } from './components/purchase/purchase.component';
import { StoreComponent } from './components/store/store.component';
import { AuthGuard } from './auth/auth.guard';

// all routes handled here
const routes: Routes = [{ path:'',component:LoginComponent},
{path:'user',component:UserComponent},
{path:'collection',component:CollectionComponent},
{path:'purchase',component:PurchaseComponent},

// guarded
{path:'store',component:StoreComponent,canActivate:[AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
