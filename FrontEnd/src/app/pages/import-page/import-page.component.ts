import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CourseInstance } from 'src/app/models/courseinstance';
import { FileUploadService } from 'src/app/services/file-upload.service';

@Component({
  selector: 'app-import-page',
  templateUrl: './import-page.component.html',
  styleUrls: ['./import-page.component.css']
})
export class ImportPageComponent {
  constructor(private fileUploadService: FileUploadService, private toastr: ToastrService) {}

  courseInstances: CourseInstance[] = [];

  ngOnInit() {
    this.toastr.toastrConfig.positionClass = 'toast-top-center'
  }

  onFileSelected(event: Event) {
    const target = event.target as HTMLInputElement;
    
    if (target.files && target.files.length > 0) {
      if (target.files.length > 1) {
        this.toastr.error("Er is maar 1 bestand tegelijkertijd toegestaan bij het importeren!");
        return;
      }

      this.fileUploadService.postFile(target.files[0])
      .subscribe(data => {
        data.courseInstances.forEach(element => {
          this.courseInstances.push(element);
        });

        if (data.courses.length == 0 && data.courseInstances.length == 0) 
        {
          this.toastr.success(`Er is geen nieuwe data geïmporteerd, omdat deze al bestond.`);
          return;
        }

        this.toastr.success(`Er zijn ${data.courses.length} nieuwe cursussen en ${data.courseInstances.length} cursusinstanties geïmporteerd.`);
      });
    }
  }
}
  
