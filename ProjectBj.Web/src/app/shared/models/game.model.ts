import { Dealer } from "./dealer.model";
import { Player } from "./player.model";

export class Game {
    sessionId: number;
    dealer: Dealer;
    player: Player;
    bots: Player[];
}
