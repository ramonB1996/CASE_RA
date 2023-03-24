import { Injectable } from "@angular/core";
import { DateDTO } from "../models/dateDTO";

@Injectable({ providedIn: 'root' })
export class DateService {
  
  calculateStartAndEndDateOfWeek(wDate: Date) : DateDTO {
    let dDay = wDate.getDay() > 0 ? wDate.getDay() : 7;
    let first = wDate.getDate() - dDay + 1;
    let firstDayWeek = new Date(wDate.setDate(first));
    let lastDayWeek = new Date(wDate.setDate(firstDayWeek.getDate()+6));

    return { startDate: firstDayWeek, endDate: lastDayWeek, year: firstDayWeek.getFullYear()};
  }

  addDays(date:Date, days:number) :Date{
    let result = new Date(date);
    result.setDate(result.getDate() + days);
    
    return result;
  }

  dateToString(date: Date) {
    return `${date.getDate()}-${date.getMonth() + 1}-${date.getFullYear()}`
  }
}