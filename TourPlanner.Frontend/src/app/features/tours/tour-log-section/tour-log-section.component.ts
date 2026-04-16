import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TourLog, Difficulty } from '../../../core/models/tourLog.model';
import { TourLogService } from '../../../core/services/tour-log.service';

@Component({
  selector: 'app-tour-log-section',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './tour-log-section.component.html',
  styleUrls: ['./tour-log-section.component.css']
})
export class TourLogSectionComponent implements OnInit {
  @Input() tourId!: number;
  @Input() logs: TourLog[] = [];
  @Output() logChanged = new EventEmitter<void>();

  logForm: FormGroup;
  isEditing = false;
  editingLogId: number | null = null;
  difficulties = Object.values(Difficulty).filter(value => typeof value === 'string');

  constructor(private fb: FormBuilder, private tourLogService: TourLogService) {
    // Validierung: Datum, Distanz, Zeit und Rating sind Pflicht
    this.logForm = this.fb.group({
      dateTime: ['', Validators.required],
      comment: [''],
      difficulty: [Difficulty.Medium, Validators.required],
      totalDistance: [0, [Validators.required, Validators.min(0.1), Validators.max(20160)]],
      totalTime: [0, [Validators.required, Validators.min(1), Validators.max(20160)]],
      rating: [5, [Validators.required, Validators.min(1), Validators.max(5)]]
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.logForm.invalid) return;

    const logData: TourLog = {
      ...this.logForm.value,
      tourId: this.tourId,
      id: this.editingLogId || 0
    };

    if (this.isEditing && this.editingLogId) {
      this.tourLogService.updateLog(logData).subscribe(() => this.onSuccess());
    } else {
      this.tourLogService.createLog(logData).subscribe(() => this.onSuccess());
    }
  }

  editLog(log: TourLog): void {
    this.isEditing = true;
    this.editingLogId = log.id;
    this.logForm.patchValue(log);
  }

  deleteLog(id: number): void {
    if (confirm('Log wirklich löschen?')) {
      this.tourLogService.deleteLog(id).subscribe(() => this.logChanged.emit());
    }
  }

  private onSuccess(): void {
    this.isEditing = false;
    this.editingLogId = null;
    this.logForm.reset({ difficulty: Difficulty.Medium, rating: 5 });
    this.logChanged.emit(); // Parent informieren, damit Daten neu geladen werden
  }

  cancel(): void {
    this.isEditing = false;
    this.editingLogId = null;
    this.logForm.reset();
  }
}