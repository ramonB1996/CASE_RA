import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CourseInstance } from 'src/app/models/courseinstance';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.css']
})
export class CoursesListComponent {
  @Input() courseInstances?: CourseInstance[];
  @Input() startDate?: Date;
  @Input() endDate?: Date;
  @Input() weekNumber?: number;
  @Input() year?: number;

  @Output() onWeekSelectedEvent = new EventEmitter<number>();

  onWeekSelected(days: number) {
    this.onWeekSelectedEvent.emit(days);
  }
}
