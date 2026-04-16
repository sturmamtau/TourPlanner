import { Component, EventEmitter, Output } from '@angular/core';
import { Tour, TransportType } from '../../../core/models/tour.model';
import { TourListComponent } from '../tour-list/tour-list.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tour-form',
  imports: [FormsModule, CommonModule],
  templateUrl: './tour-form.component.html',
  styleUrl: './tour-form.component.css'
})
export class TourFormComponent {
  @Output() formSubmitted = new EventEmitter<Tour>();
  @Output() formCancelled = new EventEmitter<void>();

  transportTypeOptions = Object.values(TransportType);

  formData: Tour = {
  id: 0,
  name: '',
  description: '',
  from: '',
  to: '',
  transportType: TransportType.Walk,
  tourDistance: 0,
  estimatedTime: 0,
  imagePath: "PLACEHOLDER_IMAGE_PATH",
  userId: 1  // später eingeloggter user
  };

  onSubmit(): void {
    // schickt die formData an die Parent-Komponente (tour-list)
    this.formSubmitted.emit(this.formData);
  }

  onCancel(): void {
    this.formCancelled.emit();
  }
}
