export class GetGameHistoryHistoryView {
    entries: EntryGetGameHistoryHistoryViewItem;
}

class EntryGetGameHistoryHistoryViewItem {
    sessionId: number;
    time: Date;
    playerName: string;
    event: string;
}