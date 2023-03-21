import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';
import { Course } from 'src/app/models/course';
import { CourseService } from 'src/app/services/course.service';
import { FileUploadService } from 'src/app/services/file-upload.service';

@Component({
  selector: 'app-courses-page',
  templateUrl: './courses-page.component.html',
  styleUrls: ['./courses-page.component.css']
})
export class CoursesPageComponent implements OnInit {
  constructor(private courseService: CourseService, private fileUploadService: FileUploadService, private toastr: ToastrService) {}

  courses: Course[] = [];

  ngOnInit() {
    this.toastr.toastrConfig.positionClass = 'toast-top-center'

    this.courseService.getAll().subscribe((courses) => {
      this.courses = courses;
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
        data.forEach(element => {
          this.courses.push(element)
        });

        this.toastr.success(`Er zijn ${data.length} nieuwe cursussen geimporteerd met elk 1 nieuwe cursusinstantie.`);
      });
    }
  }
}
