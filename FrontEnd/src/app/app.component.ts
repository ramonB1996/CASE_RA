import { Component } from '@angular/core';
import { Course } from './models/course';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'InfoSupport Cursussen';

  courses: Course[]= [
    { id: 1, title: "Programming with C#", startDate: "2020-08-31", duration: 5},
    { id: 2, title: "ASP.NET", startDate: "2020-08-31", duration: 2}
  ];
}
