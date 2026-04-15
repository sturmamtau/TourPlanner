import { Component, Input,Output, EventEmitter } from '@angular/core';
import { Tour } from '../../../core/models/tour.model';
import { CommonModule, NgIf } from '@angular/common';

@Component({
  selector: 'app-tour-detail',
  templateUrl: './tour-detail.component.html',
  styleUrls: ['./tour-detail.component.css'],
  imports: [CommonModule, NgIf]
})
export class TourDetailComponent {
  @Output() deleteTourEvent = new EventEmitter();
  @Output() updateTourEvent = new EventEmitter();
  private _tour!: Tour;

  @Input() set tour(value: Tour) {
    this._tour = value;
    console.log('Setter: Tour erhalten ->', value);
  }

  get tour(): Tour {
    return this._tour;
  }

  deleteTour(): void{
        this.deleteTourEvent.emit();
  }

  updateTour(): void{
        this.updateTourEvent.emit();
  }
}
