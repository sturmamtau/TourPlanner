import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search-bar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.css'
})
export class SearchBarComponent {

  @Input() placeholder: string = 'Search...';
  @Output() searchChanged = new EventEmitter<string>();

  searchTerm: string = '';

  onSearch(): void {
    this.searchChanged.emit(this.searchTerm);
  }

  onClear(): void {
    this.searchTerm = '';
    this.searchChanged.emit('');
  }
}