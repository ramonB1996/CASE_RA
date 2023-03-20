import { Component, OnInit } from '@angular/core';
import { Course } from './models/course';
import { CourseService } from './services/course.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private courseService: CourseService) {}

  title = 'InfoSupport Cursussen';

  courses: Course[] = [];

  ngOnInit() {
    this.courseService.getAll().subscribe((courses) => {
      this.courses = courses;
    });
  }
}
