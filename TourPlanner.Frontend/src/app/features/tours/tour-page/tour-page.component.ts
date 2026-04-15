import { Component, OnInit } from '@angular/core';
import { Tour } from '../../../core/models/tour.model';
import { TourService } from '../../../core/services/tour.service';
import { CommonModule } from '@angular/common';
import { TourFormComponent } from '../tour-form/tour-form.component';
import { TourDetailComponent } from '../tour-detail/tour-detail.component';
import { TourListComponent } from '../tour-list/tour-list.component';

@Component({
  selector: 'app-tour-page',
  imports: [CommonModule, TourFormComponent, TourDetailComponent, TourListComponent],
  templateUrl: './tour-page.component.html',
  styleUrl: './tour-page.component.css'
})
export class TourPageComponent implements OnInit {
    tours: Tour[] = []; //liste von den touren
    isLoading : boolean = false;
    errorMessage: string = "";
    formIsShown : boolean = false; //form zum updaten/neu erstellen angezeigt
    selectedTour: Tour | null = null;

    constructor(private tourService: TourService){}

    ngOnInit(): void {
      this.loadTours();
    }

    //lade liste aller touren
    loadTours(): void
    {
        this.isLoading = true;
        this.tourService.getAllTours().subscribe({
          next: (data) => {
            this.tours = data;
            this.isLoading = false
          },
          error: (err) => {
            this.errorMessage = "couldnt load tours";
            this.isLoading = false;
          }
        });
    }

    hideForm()
    {
      this.formIsShown = false;
    }
    showForm()
    {
      this.formIsShown = true;
    }

    //select tour for detal view
    onTourSelected(tour: Tour) {
      this.selectedTour = tour;
      console.log('Tour ausgewählt:', tour);
    }

    //add new tour via tour service
    addTour(formData: Tour): void
    {
      this.tourService.createTour(formData).subscribe({
        next: () => {
            this.hideForm();
            this.loadTours();
        }
      })
    }

    deleteTour(): void
    {
        console.log("tour deleted", this.selectedTour?.name);
    }

    updateTour(): void
    {
        console.log("tour updated", this.selectedTour?.name)
    }
}