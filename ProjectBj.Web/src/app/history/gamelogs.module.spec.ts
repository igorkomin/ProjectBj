import { HistoryModule } from './history.module';

describe('HistoryModule', () => {
  let gamelogsModule: HistoryModule;

  beforeEach(() => {
    gamelogsModule = new HistoryModule();
  });

  it('should create an instance', () => {
    expect(gamelogsModule).toBeTruthy();
  });
});
