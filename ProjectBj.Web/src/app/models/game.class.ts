import { Dealer } from "./dealer.class";
import { Player } from "./player.class";

export class Game {
    sessionId: number;
    dealer: Dealer;
    player: Player;
    bots: Player[];
}
