import { Component, Input, OnInit } from '@angular/core';
import { CourseInstance } from 'src/app/models/courseinstance';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.css']
})
export class CoursesListComponent {
  @Input() courseInstances?: CourseInstance[];
}
