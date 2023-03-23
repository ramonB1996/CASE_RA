import { TestBed } from "@angular/core/testing";
import { environment } from "src/environments/environment";
import { FileUploadService } from "./file-upload.service";

import {
    HttpClientTestingModule,
    HttpTestingController,
  } from '@angular/common/http/testing';
import { CourseInstance } from "../models/courseinstance";
import { firstValueFrom } from "rxjs";
import { CourseAndInstancesDTO } from "../models/courseAndInstancesDTO";

describe('FileUpload service', () => {
    let sut: FileUploadService;
    let httpMock: HttpTestingController;
    const Endpoint = `${environment.backendUrl}/courses`;
  
    beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule],
      });
      sut = TestBed.inject(FileUploadService);
      httpMock = TestBed.inject(HttpTestingController);
    });
  
    it('should not do anything when created', () => {
      httpMock.expectNone(Endpoint);
      expect().nothing();
    });
  
    describe('postFile', () => {
  
      it('should retrieve a DTO when called with a correct file', async () => {
        // Arrange
        const fakeFile = (fileName: string = 'fileName'): File => {
            const blob = new Blob([''], { type: 'text/html' });
            return <File>blob;
          };
        const expectedDTO: CourseAndInstancesDTO = 
        {
            courses: [{ id: 1, title: "Programming with C#", duration: 5, code: "CNETIN", courseInstances: []}],
            courseInstances: [{id: 1, courseId: 1,startDate: '08/10/2020', course: { id: 1, title: "Programming with C#", duration: 5, code: "CNETIN", courseInstances: []}}]
        };
    
        // Act
        const actualDTO = firstValueFrom(sut.postFile(fakeFile('example.txt')));
        httpMock.expectOne(Endpoint).flush(expectedDTO);
  
        // Assert
        expect(await actualDTO).toBe(expectedDTO);
      });
    });
  });