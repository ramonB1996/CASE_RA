import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { CourseInstance } from '../models/courseinstance';

const COURSE_API = `${environment.backendUrl}/courseinstances`;

@Injectable({ providedIn: 'root' })
export class CourseInstanceService {
  constructor(private http: HttpClient) {}

  getAll(): Observable<CourseInstance[]> {
    return this.http.get<CourseInstance[]>(COURSE_API);
  }
}