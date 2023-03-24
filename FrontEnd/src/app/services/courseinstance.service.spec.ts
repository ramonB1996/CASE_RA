import { TestBed } from "@angular/core/testing";
import { environment } from "src/environments/environment";
import { CourseInstanceService } from "./courseinstance.service";

import {
    HttpClientTestingModule,
    HttpTestingController,
  } from '@angular/common/http/testing';

describe('CourseInstance service', () => {
    let sut: CourseInstanceService;
    let httpMock: HttpTestingController;
    const courseInstancesEndpoint = `${environment.backendUrl}/courseinstances`;
  
    beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule],
      });
      sut = TestBed.inject(CourseInstanceService);
      httpMock = TestBed.inject(HttpTestingController);
    });
  
    it('should not retrieve courseinstances when created', () => {
      httpMock.expectNone(courseInstancesEndpoint);
      expect().nothing();
    });
});