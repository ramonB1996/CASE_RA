import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CourseInstance } from 'src/app/models/courseinstance';
import { StartAndEndDate } from 'src/app/models/startAndEndDate';
import { WeekAndYearSearch } from 'src/app/models/weekAndYearSearch';
import { CourseInstanceService } from 'src/app/services/courseinstance.service';

@Component({
  selector: 'app-courses-page',
  templateUrl: './courses-page.component.html',
  styleUrls: ['./courses-page.component.css']
})
export class CoursesPageComponent implements OnInit {
  constructor(private courseInstanceService: CourseInstanceService) {}

  courseInstances: CourseInstance[] = [];

  startDate?: Date;
  endDate?: Date;
  weekNumber?: number;
  year?: number;

  ngOnInit() {
    this.getDataForDate(new Date());
  }

  onWeekAndYearSearch(search: WeekAndYearSearch) {
    let date = new Date(search.year, 0, 1);
    let additionToWeek = search.isLeapYear ? 1 : 0;

    date = this.addDays(date, (search.week - additionToWeek) * 7);

    this.getDataForDate(date);
  }

  getDataForDate(date: Date) {
    let startAndEndDate = this.calculateStartAndEndDateOfThisWeek(date);
    this.startDate = startAndEndDate.startDate;
    this.endDate = startAndEndDate.endDate;
    this.weekNumber = this.weekOfYear(this.startDate);
    this.year = this.startDate.getFullYear();

    this.courseInstanceService.getAllForDateRange(this.dateToString(this.startDate), this.dateToString(this.endDate)).subscribe((courseInstances) => {
      this.courseInstances = courseInstances;
    });
  }

  toggleWeek(days:number) {
    this.startDate = this.addDays(this.startDate!, days);
    this.endDate = this.addDays(this.endDate!, days);
    this.weekNumber = this.weekOfYear(this.startDate);
    this.year = this.startDate.getFullYear();

    this.courseInstanceService.getAllForDateRange(this.dateToString(this.startDate), this.dateToString(this.endDate)).subscribe((courseInstances) => {
      this.courseInstances = courseInstances;
    });
  }

  addDays(date:Date, days:number) :Date{
    let result = new Date(date);
    result.setDate(result.getDate() + days);
    
    return result;
  }

  calculateStartAndEndDateOfThisWeek(wDate: Date) : StartAndEndDate {
    let dDay = wDate.getDay() > 0 ? wDate.getDay() : 7;
    let first = wDate.getDate() - dDay + 1;
    let firstDayWeek = new Date(wDate.setDate(first));
    let lastDayWeek = new Date(wDate.setDate(firstDayWeek.getDate()+6));

    return { startDate: firstDayWeek, endDate: lastDayWeek};
  }

  weekOfYear(date: Date): number {
    const year =  new Date(date.getFullYear(), 0, 1);
    const days =  Math.floor((+date - +year) / (24 * 60 * 60 * 1000));
    
    return Math.ceil((date.getDay() + 1 + days) / 7);
  }

  dateToString(date: Date) {
    return `${date.getDate()}-${date.getMonth() + 1}-${date.getFullYear()}`
  }
}
