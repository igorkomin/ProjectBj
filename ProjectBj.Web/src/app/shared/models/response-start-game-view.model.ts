import { CardRank } from 'src/app/shared/enums/card-rank.enum';
import { CardSuit } from 'src/app/shared/enums/card-suit.enum';

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
    suit: CardSuit;
    rank: CardRank;
}