import { NgModule } from '@angular/core';
import { GameRoutingModule } from 'src/app/game/game-routing.module';
import { GameComponent } from 'src/app/game/game/game.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    imports: [
        SharedModule,
        GameRoutingModule
    ],
    declarations: [GameComponent]
})
export class GameModule { }
