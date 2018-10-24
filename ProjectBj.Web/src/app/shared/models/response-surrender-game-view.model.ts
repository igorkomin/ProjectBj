import { CardRank } from 'src/app/shared/enums/card-rank.enum';
import { CardSuit } from 'src/app/shared/enums/card-suit.enum';

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
    suit: CardSuit;
    rank: CardRank;
}