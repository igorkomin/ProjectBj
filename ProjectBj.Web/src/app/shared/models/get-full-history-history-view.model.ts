export class GetFullHistoryHistoryView {
    entries: EntryGetFullHistoryHistoryViewItem[];
}

class EntryGetFullHistoryHistoryViewItem {
    sessionId: number;
    time: Date;
    playerName: string;
    event: string;
}