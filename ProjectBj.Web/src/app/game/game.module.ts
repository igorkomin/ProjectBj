import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { GameRoutingModule } from 'src/app/game/game-routing.module';
import { GameComponent } from 'src/app/game/game.component';

@NgModule({
    imports: [
        SharedModule,
        GameRoutingModule
    ],
    declarations: [GameComponent]
})
export class GameModule { }
