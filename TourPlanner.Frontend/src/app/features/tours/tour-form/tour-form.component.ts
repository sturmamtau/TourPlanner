import { Component, EventEmitter, Input, Output, OnChanges, SimpleChanges } from '@angular/core';
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
export class TourFormComponent implements OnChanges {
  @Input() tourToEdit: Tour | null = null;
  @Output() formSubmitted = new EventEmitter<Tour>();
  @Output() formCancelled = new EventEmitter<void>();

  transportTypeOptions = Object.values(TransportType);

  formData: any = this.getDefaultForm();

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['tourToEdit']) {
      if (this.tourToEdit) {
        // Bearbeiten-Modus: Wir kopieren die Daten der Tour in unser Formular
        this.formData = { ...this.tourToEdit };
      } else {
        // Hinzufügen-Modus: Wir leeren das Formular
        this.formData = this.getDefaultForm();
      }
    }
  }

  getDefaultForm(): any {
    return {
      id: 0,
      name: '',
      description: '',
      from: '',
      to: '',
      transportType: TransportType.Walk,
      // Die restlichen Felder werden eh vom Backend berechnet/ignoriert beim Create
    };
  }

  onSubmit(): void {
    // schickt die formData an die Parent-Komponente (tour-list)
    this.formSubmitted.emit(this.formData);
  }

  onCancel(): void {
    this.formCancelled.emit();
  }
}
