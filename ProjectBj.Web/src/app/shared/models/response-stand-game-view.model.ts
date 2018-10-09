export class ResponseStandGameView {
    sessionId: number;
    dealer: DealerResponseStandGameViewItem;
    player: PlayerResponseStandGameViewItem;
    bots: PlayerResponseStandGameViewItem[];
}

class DealerResponseStandGameViewItem {
    id: number;
    name: string;
    hand: HandResponseStandGameViewItem;
}

class PlayerResponseStandGameViewItem {
    id: number;
    name: string;
    isHuman: boolean;
    hand: HandResponseStandGameViewItem;
    gameResult: ResultResponseStandGameViewItem;
}

class ResultResponseStandGameViewItem {
    state: number;
    result: string;
}

class HandResponseStandGameViewItem {
    cards: CardResponseStandGameViewItem[];
    score: number;
}

class CardResponseStandGameViewItem {
    suit: string;
    rank: string;
    rankValue: number;
}