import { TestBed } from '@angular/core/testing';

import { PoketcgService } from './poketcg.service';

describe('PoketcgService', () => {
  let service: PoketcgService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PoketcgService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
