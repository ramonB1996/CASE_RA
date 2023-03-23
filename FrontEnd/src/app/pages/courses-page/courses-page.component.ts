import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CourseInstance } from 'src/app/models/courseinstance';
import { StartAndEndDate } from 'src/app/models/startAndEndDate';
import { CourseInstanceService } from 'src/app/services/courseinstance.service';

@Component({
  selector: 'app-courses-page',
  templateUrl: './courses-page.component.html',
  styleUrls: ['./courses-page.component.css']
})
export class CoursesPageComponent implements OnInit {
  constructor(private courseInstanceService: CourseInstanceService) {}

  startDate?: Date;
  endDate?: Date;
  courseInstances: CourseInstance[] = [];

  ngOnInit() {
    let startAndEndDate = this.calculateStartAndEndDateOfThisWeek();

    this.startDate = startAndEndDate.startDate;
    this.endDate = startAndEndDate.endDate;

    this.courseInstanceService.getAllForDateRange(this.dateToString(this.startDate), this.dateToString(this.endDate)).subscribe((courseInstances) => {
      this.courseInstances = courseInstances;
    });
  }

  togglePreviousWeek() {
    this.toggleWeek(-7);
  }

  toggleNextWeek() {
    this.toggleWeek(7);
  }

  toggleWeek(days:number) {
    this.startDate = this.addDays(this.startDate!,days);
    this.endDate = this.addDays(this.endDate!, days);

    this.courseInstanceService.getAllForDateRange(this.dateToString(this.startDate), this.dateToString(this.endDate)).subscribe((courseInstances) => {
      this.courseInstances = courseInstances;
    });
  }

  addDays(date:Date, days:number) :Date{
    let result = new Date(date);
    result.setDate(result.getDate() + days);
    
    return result;
  }

  calculateStartAndEndDateOfThisWeek() : StartAndEndDate {
    let wDate = new Date();
    let dDay = wDate.getDay() > 0 ? wDate.getDay() : 7;
    let first = wDate.getDate() - dDay + 1;
    let firstDayWeek = new Date(wDate.setDate(first));
    let lastDayWeek = new Date(wDate.setDate(firstDayWeek.getDate()+6));

    return { startDate: firstDayWeek, endDate: lastDayWeek};
  }

  dateToString(date: Date) {
    return `${date.getDate()}-${date.getMonth() + 1}-${date.getFullYear()}`
  }
}
