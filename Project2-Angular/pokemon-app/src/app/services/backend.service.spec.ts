import { TestBed } from '@angular/core/testing';

import { BackendService } from './backend.service';

describe('BackendService', () => {
  let service: BackendService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BackendService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get an user by email',() => 
  { expect(service.getUserById).toBeDefined();
  });

  it('should get an user by email',() => 
  { expect(service.getUserByEmail).toBeDefined();
  });

  it('should find the correct user',() => 
  { expect(service.getUserById('cus2')).toBeDefined();
  });

  
});
