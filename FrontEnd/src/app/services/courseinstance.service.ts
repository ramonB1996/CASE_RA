import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { CourseInstance } from '../models/courseinstance';

const COURSEINSTANCE_API = `${environment.backendUrl}/courseinstances`;

@Injectable({ providedIn: 'root' })
export class CourseInstanceService {
  constructor(private http: HttpClient) {}

  getAllForDateRange(startDate:string, endDate:string): Observable<CourseInstance[]> {
    return this.http.get<CourseInstance[]>(`${COURSEINSTANCE_API}?startDate=${startDate}&endDate=${endDate}`);
  }
}