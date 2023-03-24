import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { Output, EventEmitter } from '@angular/core';
import { CourseInstance } from 'src/app/models/courseinstance';


@Component({
  selector: 'app-courses-import',
  templateUrl: './courses-import.component.html',
  styleUrls: ['./courses-import.component.css']
})

export class CoursesImportComponent 
{  
  @Input() courseInstances?: CourseInstance[];

  @Output() fileSelectedEvent = new EventEmitter<Event>();
  @ViewChild('fileUploader') fileUploader?:ElementRef;

  onFileSelected(event: Event) {
    this.fileSelectedEvent.emit(event); 

    // Reset input type=file
    this.fileUploader!.nativeElement.value = null;
  }
}
