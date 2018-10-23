export class ResponseSurrenderGameView {
    sessionId: number;
    dealer: DealerResponseSurrenderGameViewItem;
    player: PlayerResponseSurrenderGameViewItem;
    bots: PlayerResponseSurrenderGameViewItem[];
}

class DealerResponseSurrenderGameViewItem {
    id: number;
    name: string;
    hand: HandResponseSurrenderGameViewItem;
}

class PlayerResponseSurrenderGameViewItem {
    id: number;
    name: string;
    isHuman: boolean;
    hand: HandResponseSurrenderGameViewItem;
    gameResult: ResultResponseSurrenderGameViewItem;
}

class ResultResponseSurrenderGameViewItem {
    state: number;
    result: string;
}

class HandResponseSurrenderGameViewItem {
    cards: CardResponseSurrenderGameViewItem[];
    score: number;
}

class CardResponseSurrenderGameViewItem {
    suit: string;
    rank: string;
}