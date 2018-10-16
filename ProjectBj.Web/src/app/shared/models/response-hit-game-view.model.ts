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
    isHuman: boolean;
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
    suit: string;
    rank: string;
    rankValue: number;
}