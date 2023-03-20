import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/models/course';
import { CourseService } from 'src/app/services/course.service';

@Component({
  selector: 'app-courses-page',
  templateUrl: './courses-page.component.html',
  styleUrls: ['./courses-page.component.css']
})
export class CoursesPageComponent implements OnInit {
  constructor(private courseService: CourseService) {}

  courses: Course[] = [];

  ngOnInit() {
    this.courseService.getAll().subscribe((courses) => {
      this.courses = courses;
    });
  }
}
