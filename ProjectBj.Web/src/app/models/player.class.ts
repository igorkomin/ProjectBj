import { Hand } from "./hand.class";

export class Player {
    id: number;
    name: string;
    isHuman: boolean;
    inGame: boolean;
    balance: number;
    gameResult: number;
    bet: number;
    hand: Hand;
}
