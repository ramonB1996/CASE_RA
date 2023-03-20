import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/models/course';
import { CourseService } from 'src/app/services/course.service';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.css']
})
export class CoursesListComponent {
  constructor(private courseService: CourseService) {}

  courses: Course[] = [];

  ngOnInit() {
    this.courseService.getAll().subscribe((courses) => {
      this.courses = courses;
    });
  }
}
