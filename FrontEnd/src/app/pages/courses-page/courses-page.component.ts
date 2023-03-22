import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';
import { CourseInstance } from 'src/app/models/courseinstance';
import { CourseInstanceService } from 'src/app/services/courseinstance.service';
import { FileUploadService } from 'src/app/services/file-upload.service';

@Component({
  selector: 'app-courses-page',
  templateUrl: './courses-page.component.html',
  styleUrls: ['./courses-page.component.css']
})
export class CoursesPageComponent implements OnInit {
  constructor(private courseInstanceService: CourseInstanceService, private fileUploadService: FileUploadService, private toastr: ToastrService) {}

  courseInstances: CourseInstance[] = [];

  ngOnInit() {
    this.toastr.toastrConfig.positionClass = 'toast-top-center'

    this.courseInstanceService.getAll().subscribe((courseInstances) => {
      this.courseInstances = courseInstances;
    });
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
