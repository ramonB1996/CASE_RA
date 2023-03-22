import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { Course } from '../models/course';
import { CourseAndInstancesDTO } from '../models/courseAndInstancesDTO';

const COURSE_API = `${environment.backendUrl}/courses`;

@Injectable({ providedIn: 'root' })
export class FileUploadService {
  constructor(private http: HttpClient) {}

  postFile(fileToUpload: File): Observable<CourseAndInstancesDTO> {
    let formData = new FormData();
    formData.set("file", fileToUpload);
    
    return this.http.post<CourseAndInstancesDTO>(COURSE_API, formData);        
  }
}