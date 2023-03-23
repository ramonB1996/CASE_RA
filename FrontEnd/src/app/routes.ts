import { Route } from '@angular/router';
import { CoursesPageComponent } from './pages/courses-page/courses-page.component';
import { ImportPageComponent } from './pages/import-page/import-page.component';

export const routes: Route[] = [
  { path: 'courses', component: CoursesPageComponent },
  { path: 'import', component: ImportPageComponent },
  { path: '', pathMatch: 'full', redirectTo: '/courses' },
];