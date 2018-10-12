export class GetFullLogView {
    entries: GetFullLogViewItem[];
}

class GetFullLogViewItem {
    machineName: string;
    siteName: string;
    creationDate: Date;
    level: string;
    userName: string;
    message: string;
    logger: string;
    properties: string;
    serverName: string;
    port: string;
    url: string;
    https: boolean;
    serverAddress: string;
    remoteAddress: string;
    callSite: string;
    exception: string;
}