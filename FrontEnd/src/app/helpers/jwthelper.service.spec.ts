import { TestBed } from '@angular/core/testing';

import { JWTHelperService } from './jwthelper.service';

describe('JwthelperService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: JWTHelperService = TestBed.get(JWTHelperService);
    expect(service).toBeTruthy();
  });
});
