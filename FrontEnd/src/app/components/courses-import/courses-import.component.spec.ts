import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoursesImportComponent } from './courses-import.component';

describe('CoursesImportComponent', () => {
  let component: CoursesImportComponent;
  let fixture: ComponentFixture<CoursesImportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CoursesImportComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CoursesImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
