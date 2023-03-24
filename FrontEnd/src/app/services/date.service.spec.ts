import { TestBed } from "@angular/core/testing";
import { environment } from "src/environments/environment";
import { CourseInstanceService } from "./courseinstance.service";

import {
    HttpClientTestingModule,
    HttpTestingController,
  } from '@angular/common/http/testing';
import { CourseInstance } from "../models/courseinstance";
import { firstValueFrom } from "rxjs";
import { DateService } from "./date.service";

describe('Date service', () => {
    let sut: DateService;
  
    beforeEach(() => {
      sut = TestBed.inject(DateService);
    });
  
    it('calculateStartAndEndDateOfWeek should retrieve correct data', () => {
        let startDateExpectation = new Date(2023, 2, 20);
        let endDateExpectation = new Date(2023, 2, 26);

        let result = sut.calculateStartAndEndDateOfWeek(new Date(2023, 2, 24));
       
        expect(result.startDate.getDate()).toBe(startDateExpectation.getDate());
        expect(result.endDate.getDate()).toBe(endDateExpectation.getDate());
        expect(result.year).toBe(2023);
    });

    it('addDays should return date + days', () => {
        let date1 = new Date(2023,2,20);
        let date2 = new Date(2023,2,27);

        let result = sut.addDays(date1, 7);

        expect(result.getDate()).toBe(date2.getDate());
    });

    it('addDays should return date - days', () => {
        let date1 = new Date(2023,2,20);
        let date2 = new Date(2023,2,13);

        let result = sut.addDays(date1, -7);

        expect(result.getDate()).toBe(date2.getDate());
    });

    it('dateToString should return date as string', () => {
        let date = new Date(2023, 2, 24);
        let date2 = new Date(2023,0,1);

        let result = sut.dateToString(date);
        let result2 = sut.dateToString(date2);
        
        expect(result).toBe('24-3-2023');
        expect(result2).toBe('1-1-2023');
    });
});