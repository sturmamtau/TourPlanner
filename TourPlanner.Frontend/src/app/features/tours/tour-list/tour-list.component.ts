import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
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
    @Input() tours: Tour[] = []; //liste von den touren
    @Input() isLoading: boolean = false;
    @Input() errorMessage: string = "";

    @Output() tourSelected = new EventEmitter<Tour>();
    @Output() showForm = new EventEmitter<void>();

    constructor(private tourService: TourService){}

    ngOnInit(): void {
      // Removed loadTours() as tours are now passed as input
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
