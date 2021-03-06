﻿import { CardRank } from 'src/app/shared/enums/card-rank.enum';
import { CardSuit } from 'src/app/shared/enums/card-suit.enum';

export class ResponseHitGameView {
    sessionId: number;
    dealer: DealerResponseHitGameViewItem;
    player: PlayerResponseHitGameViewItem;
    bots: PlayerResponseHitGameViewItem[];
}

class DealerResponseHitGameViewItem {
    id: number;
    name: string;
    hand: HandResponseHitGameViewItem;
}

class PlayerResponseHitGameViewItem {
    id: number;
    name: string;
    hand: HandResponseHitGameViewItem;
    gameResult: ResultResponseHitGameViewItem;
}

class ResultResponseHitGameViewItem {
    state: number;
    result: string;
}

class HandResponseHitGameViewItem {
    cards: CardResponseHitGameViewItem[];
    score: number;
}

class CardResponseHitGameViewItem {
    suit: CardSuit;
    rank: CardRank;
}