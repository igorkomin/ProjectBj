export class HistoryView {
    entries: HistoryViewItem[];
}

class HistoryViewItem {
    sessionId: number;
    time: Date;
    playerName: string;
    event: string;
}
