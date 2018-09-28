import { Hand } from "./hand.model";
import { GameResult } from "./game-result.model";

export class Player {
    id: number;
    name: string;
    isHuman: boolean;
    inGame: boolean;
    gameResult: GameResult;
    hand: Hand;
}
