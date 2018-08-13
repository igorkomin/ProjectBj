import { HandModel } from "./hand.model";

export class PlayerModel {
    id: number;
    name: string;
    isHuman: boolean;
    inGame: boolean;
    balance: number;
    gameResult: number;
    bet: number;
    hand: HandModel;
}
