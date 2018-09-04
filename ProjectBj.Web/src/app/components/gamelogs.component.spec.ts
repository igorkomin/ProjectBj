import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GamelogsComponent } from './gamelogs.component';

describe('GamelogsComponent', () => {
  let component: GamelogsComponent;
  let fixture: ComponentFixture<GamelogsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GamelogsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GamelogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
