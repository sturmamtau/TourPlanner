import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Tour } from '../../../core/models/tour.model';
import { CommonModule, NgIf } from '@angular/common';

@Component({
  selector: 'app-tour-detail',
  templateUrl: './tour-detail.component.html',
  styleUrls: ['./tour-detail.component.css'],
  imports: [CommonModule, NgIf]
})
export class TourDetailComponent {
  private _tour!: Tour;

  @Input() set tour(value: Tour) {
    this._tour = value;
    console.log('Setter: Tour erhalten ->', value);
    // Hier kannst du weiteren Code ausführen, der sofort bei Änderung
    // passieren soll, z.B. eine Karte neu laden.
  }

  get tour(): Tour {
    return this._tour;
  }
}
