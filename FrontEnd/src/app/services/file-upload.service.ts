import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { Course } from '../models/course';

const COURSE_API = `${environment.backendUrl}/courses`;

@Injectable({ providedIn: 'root' })
export class FileUploadService {
  constructor(private http: HttpClient) {}

  postFile(fileToUpload: File): Observable<Course[]> {
    let formData = new FormData();
    formData.set("file", fileToUpload);
    
    return this.http.post<Course[]>(COURSE_API, formData);        
  }
}