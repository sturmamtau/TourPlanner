import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Tour } from '../../../core/models/tour.model';
import { TourService } from '../../../core/services/tour.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tour-list',
  imports: [CommonModule],
  templateUrl: './tour-list.component.html',
  styleUrl: './tour-list.component.css'
})
export class TourListComponent implements OnInit {
    tours: Tour[] = []; //liste von den touren
    isLoading : boolean = false;
    errorMessage: string = "";

    @Output() tourSelected = new EventEmitter<Tour>();
    @Output() showForm = new EventEmitter<void>();

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

    //select tour for detal view
    selectTour(tour: Tour) {
      this.tourSelected.emit(tour);
      console.log('Tour ausgewählt:', tour);
    }

    onShowForm() {
      this.showForm.emit();
    }
}
