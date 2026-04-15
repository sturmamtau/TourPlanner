import { Component, Input,Output, EventEmitter } from '@angular/core';
import { Tour } from '../../../core/models/tour.model';
import { CommonModule, NgIf } from '@angular/common';
import { TourLogSectionComponent } from '../tour-log-section/tour-log-section.component';

@Component({
  selector: 'app-tour-detail',
  templateUrl: './tour-detail.component.html',
  styleUrls: ['./tour-detail.component.css'],
  imports: [CommonModule, NgIf, TourLogSectionComponent]
})
export class TourDetailComponent {
  @Output() deleteTourEvent = new EventEmitter();
  @Output() updateTourEvent = new EventEmitter<void>();
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

  onLogChanged() {
    console.log('Detail: Log wurde geändert, informiere Page...');
    this.updateTourEvent.emit();
  }
}
