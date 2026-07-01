import { TestBed } from '@angular/core/testing';

import { MapSnapshotService } from './map-snapshot.service';

describe('MapSnapshotService', () => {
  let service: MapSnapshotService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MapSnapshotService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
