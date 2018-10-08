export class ResponseGameView {
    sessionId: number;
    dealer: DealerResponseGameViewItem;
    player: PlayerResponseGameViewItem;
    bots: PlayerResponseGameViewItem[];
}

export class DealerResponseGameViewItem {
    id: number;
    name: string;
    hand: HandResponseGameViewItem;
}

export class PlayerResponseGameViewItem {
    id: number;
    name: string;
    isHuman: boolean;
    hand: HandResponseGameViewItem;
    gameResult: ResultResponseGameViewItem;
}

export class ResultResponseGameViewItem {
    state: number;
    result: string;
}

export class HandResponseGameViewItem {
    cards: CardResponseGameViewItem[];
    score: number;
}

export class CardResponseGameViewItem {
    suit: string;
    rank: string;
    rankValue: number;
}