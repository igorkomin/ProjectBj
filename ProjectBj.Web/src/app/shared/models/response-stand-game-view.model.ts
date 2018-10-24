import { CardRank } from 'src/app/shared/enums/card-rank.enum';
import { CardSuit } from 'src/app/shared/enums/card-suit.enum';

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
    suit: CardSuit;
    rank: CardRank;
}