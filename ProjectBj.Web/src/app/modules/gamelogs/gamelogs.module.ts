import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { GamelogsRoutingModule } from './gamelogs-routing.module';
import { GamelogsComponent } from 'src/app/components/gamelogs/gamelogs.component';
import { SharedModule } from 'src/app/modules/shared.module';

@NgModule({
    imports: [
        SharedModule,
        GamelogsRoutingModule,
        GridModule
    ],
    declarations: [GamelogsComponent]
})
export class GamelogsModule { }
