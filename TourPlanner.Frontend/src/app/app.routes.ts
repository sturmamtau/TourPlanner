import { Routes } from '@angular/router';
import { TourListComponent } from './features/tours/tour-list/tour-list.component';

export const routes: Routes = [
    { path:'', redirectTo: 'tours', pathMatch: 'full'},
    {path: 'tours', component: TourListComponent}
];
