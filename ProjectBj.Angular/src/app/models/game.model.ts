import { DealerModel } from "./dealer.model";
import { PlayerModel } from "./player.model";

export class GameModel {
    sessionId: number;
    dealer: DealerModel;
    player: PlayerModel;
    bots: PlayerModel[];
}
