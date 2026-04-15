import { Component, OnInit } from '@angular/core';
import { Tour } from '../../../core/models/tour.model';
import { TourService } from '../../../core/services/tour.service';
import { CommonModule } from '@angular/common';
import { TourFormComponent } from '../tour-form/tour-form.component';
import { TourDetailComponent } from '../tour-detail/tour-detail.component';

@Component({
  selector: 'app-tour-list',
  imports: [CommonModule, TourFormComponent, TourDetailComponent],
  templateUrl: './tour-list.component.html',
  styleUrl: './tour-list.component.css'
})
export class TourListComponent implements OnInit {
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
    selectTour(tour: Tour) {
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
}
