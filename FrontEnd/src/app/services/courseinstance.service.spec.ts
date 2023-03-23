import { TestBed } from "@angular/core/testing";
import { environment } from "src/environments/environment";
import { CourseInstanceService } from "./courseinstance.service";

import {
    HttpClientTestingModule,
    HttpTestingController,
  } from '@angular/common/http/testing';
import { CourseInstance } from "../models/courseinstance";
import { firstValueFrom } from "rxjs";

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
  
    describe('getAll', () => {
  
      it('should retrieve all courseinstances when called', async () => {
        // Arrange
        const expectedCourseInstances: CourseInstance[] = [
          { id: 1, courseId: 1, startDate: '08/10/2020', course: { id: 1, title: "Programming with C#", duration: 5, code: "CNETIN", courseInstances: []} },
        ];
  
        // Act
        const actualCourseInstancesAct = firstValueFrom(sut.getAll());
        httpMock.expectOne(courseInstancesEndpoint).flush(expectedCourseInstances);
  
        // Assert
        expect(await actualCourseInstancesAct).toBe(expectedCourseInstances);
      });
    });
  });