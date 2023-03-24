import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CourseInstance } from 'src/app/models/courseinstance';
import { WeekAndYearSearch } from 'src/app/models/weekAndYearSearch';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.css']
})
export class CoursesListComponent {
  constructor(private toastr: ToastrService) {}

  @Input() courseInstances?: CourseInstance[];
  @Input() startDate?: Date;
  @Input() endDate?: Date;
  @Input() weekNumber?: number;
  @Input() year?: number;

  @Output() onWeekSelectedEvent = new EventEmitter<number>();
  @Output() onWeekAndYearSearchedEvent = new EventEmitter<WeekAndYearSearch>();

  onWeekSelected(days: number) {
    this.onWeekSelectedEvent.emit(days);
  }

  ngOnInit() {
    this.toastr.toastrConfig.positionClass = 'toast-top-center';
  }

  onWeekAndYearSearched(week: string, year: string) {
    let weekNumber = +week;
    let yearNumber = +year;

    this.toastr.clear();
    
    if (weekNumber < 1 || weekNumber > 53) {
      this.toastr.error("Weeknummer moet tussen 1 en 53 zijn.");
      return;
    }

    const isLeapYear = new Date(yearNumber, 1, 29).getDate() === 29;

    if (weekNumber == 53 && !isLeapYear) {
      this.toastr.error("Weeknummer mag alleen 53 zijn bij een schrikkeljaar.");
      return;
    }

    this.onWeekAndYearSearchedEvent.emit({ week: weekNumber, year: yearNumber, isLeapYear: isLeapYear});
  }
}
