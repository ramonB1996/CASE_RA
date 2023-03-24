import { Injectable } from "@angular/core";
import { DateDTO } from "../models/dateDTO";

@Injectable({ providedIn: 'root' })
export class DateService {
  constructor() {}

  getDataForDate(date: Date): DateDTO {
    let startAndEndDate = this.calculateStartAndEndDateOfWeek(date);

    return { startDate: startAndEndDate.startDate, 
            endDate:  startAndEndDate.endDate, 
            week: this.weekOfYear(startAndEndDate.startDate),
            year: startAndEndDate.startDate.getFullYear()};
  }

  addDays(date:Date, days:number) :Date{
    let result = new Date(date);
    result.setDate(result.getDate() + days);
    
    return result;
  }

  weekOfYear(date: Date): number {
    const year =  new Date(date.getFullYear(), 0, 1);
    const days =  Math.floor((+date - +year) / (24 * 60 * 60 * 1000));
    
    return Math.ceil((date.getDay() + 1 + days) / 7);
  }

  dateToString(date: Date) {
    return `${date.getDate()}-${date.getMonth() + 1}-${date.getFullYear()}`
  }

  private calculateStartAndEndDateOfWeek(wDate: Date) : DateDTO {
    let dDay = wDate.getDay() > 0 ? wDate.getDay() : 7;
    let first = wDate.getDate() - dDay + 1;
    let firstDayWeek = new Date(wDate.setDate(first));
    let lastDayWeek = new Date(wDate.setDate(firstDayWeek.getDate()+6));

    return { startDate: firstDayWeek, endDate: lastDayWeek, week: 0, year: 0};
  }
}