import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoursesListComponent } from './components/courses-list/courses-list.component';
import { CoursesPageComponent } from './pages/courses-page/courses-page.component';
import { CoursesImportComponent } from './components/courses-import/courses-import.component';

@NgModule({
  declarations: [
    AppComponent,
    CoursesListComponent,
    CoursesPageComponent,
    CoursesImportComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
