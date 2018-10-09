export class ResponseStartGameView {
    sessionId: number;
    dealer: DealerResponseStartGameViewItem;
    player: PlayerResponseStartGameViewItem;
    bots: PlayerResponseStartGameViewItem[];
}

class DealerResponseStartGameViewItem {
    id: number;
    name: string;
    hand: HandResponseStartGameViewItem;
}

class PlayerResponseStartGameViewItem {
    id: number;
    name: string;
    isHuman: boolean;
    hand: HandResponseStartGameViewItem;
    gameResult: ResultResponseStartGameViewItem;
}

class ResultResponseStartGameViewItem {
    state: number;
    result: string;
}

class HandResponseStartGameViewItem {
    cards: CardResponseStartGameViewItem[];
    score: number;
}

class CardResponseStartGameViewItem {
    suit: string;
    rank: string;
    rankValue: number;
}