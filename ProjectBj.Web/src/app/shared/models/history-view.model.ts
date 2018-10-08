export class HistoryView {
    entries: HistoryViewItem[];
}

export class HistoryViewItem {
    sessionId: number;
    time: Date;
    playerName: string;
    event: string;
}
