import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Course } from '../models/course';

const COURSE_API = `${environment.backendUrl}/courses`;

@Injectable({ providedIn: 'root' })
export class FileUploadService {
  constructor(private http: HttpClient) {}

  postFile(fileToUpload: File | null) :Observable<boolean>{
    let headers = new Headers();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'text/plain');
    
    let options = new RequestOptions({ headers: headers });
    this.http.post(`${this.apiEndPoint}`, formData, options)
        .map(res => res.json())
        .catch(error => Observable.throw(error))
        .subscribe(
            data => console.log('success'),
            error => console.log(error)
        )
  }
}