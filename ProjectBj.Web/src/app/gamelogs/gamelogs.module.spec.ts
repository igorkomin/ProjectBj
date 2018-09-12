import { GamelogsModule } from './gamelogs.module';

describe('GamelogsModule', () => {
  let gamelogsModule: GamelogsModule;

  beforeEach(() => {
    gamelogsModule = new GamelogsModule();
  });

  it('should create an instance', () => {
    expect(gamelogsModule).toBeTruthy();
  });
});
