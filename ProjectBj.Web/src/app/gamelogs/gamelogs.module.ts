import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { GamelogsRoutingModule } from 'src/app/gamelogs/gamelogs-routing.module';
import { GamelogsComponent } from 'src/app/gamelogs/gamelogs.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    imports: [
        SharedModule,
        GamelogsRoutingModule,
        GridModule
    ],
    declarations: [GamelogsComponent]
})
export class GamelogsModule { }
