import { HandModel } from "./hand.model";

export class DealerModel {
    id: number;
    name: string;
    inGame: boolean;
    hand: HandModel;
}
