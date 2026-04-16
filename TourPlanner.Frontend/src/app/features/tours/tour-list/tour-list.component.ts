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

    selectedTourId: number | null = null;

    constructor(private tourService: TourService){}

    ngOnInit(): void {
        
    }

    //select tour for detal view
    selectTour(tour: Tour) {
      this.selectedTourId = tour.id;
      this.tourSelected.emit(tour);
      console.log('Tour ausgewählt:', tour);
    }

    onShowForm() {
      this.selectedTourId = null;
      this.showForm.emit();
    }
}
