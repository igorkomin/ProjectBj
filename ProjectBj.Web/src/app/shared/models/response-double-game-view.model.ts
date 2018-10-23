﻿export class ResponseDoubleGameView {
    sessionId: number;
    dealer: DealerResponseDoubleGameViewItem;
    player: PlayerResponseDoubleGameViewItem;
    bots: PlayerResponseDoubleGameViewItem[];
}

class DealerResponseDoubleGameViewItem {
    id: number;
    name: string;
    hand: HandResponseDoubleGameViewItem;
}

class PlayerResponseDoubleGameViewItem {
    id: number;
    name: string;
    isHuman: boolean;
    hand: HandResponseDoubleGameViewItem;
    gameResult: ResultResponseDoubleGameViewItem;
}

class ResultResponseDoubleGameViewItem {
    state: number;
    result: string;
}

class HandResponseDoubleGameViewItem {
    cards: CardResponseDoubleGameViewItem[];
    score: number;
}

class CardResponseDoubleGameViewItem {
    suit: string;
    rank: string;
}