import { Component, OnInit } from '@angular/core';
import { CourseInstance } from 'src/app/models/courseinstance';
import { DateDTO } from 'src/app/models/dateDTO';
import { WeekAndYearSearch } from 'src/app/models/weekAndYearSearch';
import { CourseInstanceService } from 'src/app/services/courseinstance.service';
import { DateService } from 'src/app/services/date.service';

@Component({
  selector: 'app-courses-page',
  templateUrl: './courses-page.component.html',
  styleUrls: ['./courses-page.component.css']
})
export class CoursesPageComponent implements OnInit {
  constructor(private courseInstanceService: CourseInstanceService, private dateService: DateService) {}

  courseInstances: CourseInstance[] = [];

  startDate?: Date;
  endDate?: Date;
  year?: number;

  ngOnInit() {
    let result = this.dateService.calculateStartAndEndDateOfWeek(new Date());
    this.refreshPropertyValues(result);
  }

  onWeekAndYearSearch(search: WeekAndYearSearch) {
    let date = new Date(search.year, 0, 1);
    let additionToWeek = search.isLeapYear ? 1 : 0;

    date = this.dateService.addDays(date, (search.week - additionToWeek) * 7);

    let result = this.dateService.calculateStartAndEndDateOfWeek(date);
    this.refreshPropertyValues(result);
  }

  toggleWeek(days:number) {
    this.refreshPropertyValues({
      startDate: this.dateService.addDays(this.startDate!, days),
      endDate: this.dateService.addDays(this.endDate!, days),
      year: this.startDate!.getFullYear()});
  }

  private refreshPropertyValues(result: DateDTO) {
    this.startDate = result.startDate;
    this.endDate = result.endDate;
    this.year = result.year;

    this.retrieveDataFromServer();
  }

  private retrieveDataFromServer() {
    this.courseInstanceService.getAllForDateRange(this.dateService.dateToString(this.startDate!), this.dateService.dateToString(this.endDate!)).subscribe((courseInstances) => {
      this.courseInstances = courseInstances;
    });
  }
}
