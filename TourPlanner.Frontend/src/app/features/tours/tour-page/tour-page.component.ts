import { Component, OnInit } from '@angular/core';
import { Tour } from '../../../core/models/tour.model';
import { TourService } from '../../../core/services/tour.service';
import { CommonModule } from '@angular/common';
import { TourFormComponent } from '../tour-form/tour-form.component';
import { TourDetailComponent } from '../tour-detail/tour-detail.component';
import { TourListComponent } from '../tour-list/tour-list.component';
import { SearchBarComponent } from '../../../shared/components/search-bar/search-bar.component';
import { MapSnapshotService } from '../../../core/services/map-snapshot.service';

@Component({
  selector: 'app-tour-page',
  imports: [CommonModule, TourFormComponent, TourDetailComponent, TourListComponent, SearchBarComponent],
  templateUrl: './tour-page.component.html',
  styleUrl: './tour-page.component.css'
})
export class TourPageComponent implements OnInit {
    tours: Tour[] = []; //liste von den touren
    isLoading : boolean = false;
    errorMessage: string = "";
    formIsShown : boolean = false; //form zum updaten/neu erstellen angezeigt
    selectedTour: Tour | null = null;
    

    constructor(
      private tourService: TourService,
      private mapSnapshotService: MapSnapshotService
    ){}

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

    hideForm()
    {
      this.formIsShown = false;
    }
    showForm()
    {
      this.formIsShown = true;
    }

    //select tour for detal view
    onTourSelected(tour: Tour) {
      this.selectedTour = tour;
      console.log('Tour ausgewählt:', tour);
    }

    onSearch(search: string){
      console.log(`searching for ${search}`)
    }


    formSubmit(formData: any){
        const createTourDto = {
            name: formData.name,
            description: formData.description || '',
            from: formData.from,
            to: formData.to,
            transportType: formData.transportType
        };

        if (formData.id && formData.id > 0) {
            this.updateTour(formData.id, createTourDto);
        } else {
            this.addTour(createTourDto);
        }
    }

    //add new tour via tour service
    addTour(tourDto: any): void {
      console.log("Füge neue Tour hinzu...", tourDto);
      this.tourService.createTour(tourDto).subscribe({
        next: async (newTour) => {
            this.hideForm();

            if (newTour.routeGeoJson) {
              try {
                const snapshotBlob = await this.mapSnapshotService.createSnapshot(JSON.parse(newTour.routeGeoJson));
                await this.tourService.uploadTourImage(newTour.id, snapshotBlob).toPromise();
              } catch (error) {
                console.error('Fehler beim Erzeugen oder Upload des Kartenbildes:', error);
              }
            }

            this.loadTours();
        },
        error: (err) => console.error("Fehler beim Erstellen der Tour:", err)
      });
    }

    updateTour(id: number, tourDto: any): void {
      console.log("Aktualisiere Tour...", id, tourDto);
      
      // ACHTUNG: Dein TourService muss updateTour(id, dto) unterstützen!
      this.tourService.updateTour(id, tourDto).subscribe({
        next: () => {
          this.hideForm();

          this.refreshSelectedTour(); 
        },
        error: (err) => console.error("Fehler beim Updaten der Tour:", err)
      });
    }
    
   deleteTour(): void {
      if (this.selectedTour) {
        this.tourService.deleteTour(this.selectedTour.id).subscribe({
          next: () => {
              this.selectedTour = null; 
              this.loadTours();
          },
          error: (err) => console.error("Fehler beim Löschen:", err)
        });
      }
    }

  refreshSelectedTour(): void {
      this.isLoading = true;
      this.tourService.getAllTours().subscribe({
        next: (data) => {
          this.tours = data;
          if (this.selectedTour) {
            const found = this.tours.find(t => t.id === this.selectedTour?.id);
            this.selectedTour = found ? found : null;
          }
          this.isLoading = false;
        },
        error: () => {
          this.errorMessage = "Fehler beim Aktualisieren";
          this.isLoading = false;
        }
      });
    }
}