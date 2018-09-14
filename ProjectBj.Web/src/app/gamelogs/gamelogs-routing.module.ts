import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GamelogsComponent } from 'src/app/gamelogs/gamelogs.component';

const routes: Routes = [
    {
        path: '',
        component: GamelogsComponent
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GamelogsRoutingModule { }