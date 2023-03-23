import { Component, OnInit } from '@angular/core';
import { CourseInstance } from 'src/app/models/courseinstance';
import { CourseInstanceService } from 'src/app/services/courseinstance.service';

@Component({
  selector: 'app-courses-page',
  templateUrl: './courses-page.component.html',
  styleUrls: ['./courses-page.component.css']
})
export class CoursesPageComponent implements OnInit {
  constructor(private courseInstanceService: CourseInstanceService) {}

  courseInstances: CourseInstance[] = [];

  ngOnInit() {
    this.courseInstanceService.getAll().subscribe((courseInstances) => {
      this.courseInstances = courseInstances;
    });
  }

  
}
