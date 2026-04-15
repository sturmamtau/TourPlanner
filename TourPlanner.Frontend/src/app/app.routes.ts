import { Routes } from '@angular/router';
import { TourPageComponent } from './features/tours/tour-page/tour-page.component';

export const routes: Routes = [
    { path:'', redirectTo: 'tours', pathMatch: 'full'},
    {path: 'tours', component: TourPageComponent}
];
