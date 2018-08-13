import { NgModule } from '@angular/core';
import { RouterModule, Routes, Router } from '@angular/router'

import { LoginComponent } from './components/login/login.component'
import { GameComponent } from './components/game/game.component'

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'game', component: GameComponent }
]

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
