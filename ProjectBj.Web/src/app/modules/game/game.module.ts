import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/modules/shared/shared.module';
import { GameRoutingModule } from 'src/app/modules/game/game-routing.module';
import { GameComponent } from 'src/app/components/game/game.component';

@NgModule({
    imports: [
        SharedModule,
        GameRoutingModule
    ],
    declarations: [GameComponent]
})
export class GameModule { }
