import { HttpClient } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { FileUploadService } from 'src/app/services/file-upload.service';

@Component({
  selector: 'app-courses-import',
  templateUrl: './courses-import.component.html',
  styleUrls: ['./courses-import.component.css']
})
export class CoursesImportComponent {
  constructor(private fileUploadService: FileUploadService) {}
  
  onFileSelected(event: Event) {
    const target = event.target as HTMLInputElement;
    
    if (target.files && target.files.length > 0) {
      this.fileUploadService.postFile(target.files[0]);
    }
  }
}
