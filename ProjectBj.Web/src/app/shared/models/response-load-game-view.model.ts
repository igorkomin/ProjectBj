export class ResponseLoadGameView {
    sessionId: number;
    dealer: DealerResponseLoadGameViewItem;
    player: PlayerResponseLoadGameViewItem;
    bots: PlayerResponseLoadGameViewItem[];
}

class DealerResponseLoadGameViewItem {
    id: number;
    name: string;
    hand: HandResponseLoadGameViewItem;
}

class PlayerResponseLoadGameViewItem {
    id: number;
    name: string;
    isHuman: boolean;
    hand: HandResponseLoadGameViewItem;
    gameResult: ResultResponseLoadGameViewItem;
}

class ResultResponseLoadGameViewItem {
    state: number;
    result: string;
}

class HandResponseLoadGameViewItem {
    cards: CardResponseLoadGameViewItem[];
    score: number;
}

class CardResponseLoadGameViewItem {
    suit: string;
    rank: string;
}