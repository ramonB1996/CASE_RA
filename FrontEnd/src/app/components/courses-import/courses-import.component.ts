import { Component, ElementRef, ViewChild } from '@angular/core';
import { Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-courses-import',
  templateUrl: './courses-import.component.html',
  styleUrls: ['./courses-import.component.css']
})

export class CoursesImportComponent 
{  
  @Output() fileSelectedEvent = new EventEmitter<Event>();
  @ViewChild('fileUploader') fileUploader?:ElementRef;

  onFileSelected(event: Event) {
    this.fileSelectedEvent.emit(event); 

    // Reset input type=file
    this.fileUploader!.nativeElement.value = null;
  }
}
