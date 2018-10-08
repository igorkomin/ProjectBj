export class ResponseGameView {
    sessionId: number;
    dealer: DealerResponseGameViewItem;
    player: PlayerResponseGameViewItem;
    bots: PlayerResponseGameViewItem[];
}

class DealerResponseGameViewItem {
    id: number;
    name: string;
    hand: HandResponseGameViewItem;
}

class PlayerResponseGameViewItem {
    id: number;
    name: string;
    isHuman: boolean;
    hand: HandResponseGameViewItem;
    gameResult: ResultResponseGameViewItem;
}

class ResultResponseGameViewItem {
    state: number;
    result: string;
}

class HandResponseGameViewItem {
    cards: CardResponseGameViewItem[];
    score: number;
}

class CardResponseGameViewItem {
    suit: string;
    rank: string;
    rankValue: number;
}