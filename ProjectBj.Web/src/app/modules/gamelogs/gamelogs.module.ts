import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { GamelogsRoutingModule } from './gamelogs-routing.module';
import { GamelogsComponent } from './../../components/gamelogs/gamelogs.component';
import { SharedModule } from './../shared.module';

@NgModule({
    imports: [
        SharedModule,
        GamelogsRoutingModule,
        GridModule
    ],
    declarations: [GamelogsComponent]
})
export class GamelogsModule { }
