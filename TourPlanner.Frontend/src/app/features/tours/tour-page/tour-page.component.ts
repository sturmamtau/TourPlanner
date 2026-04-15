import { Component, OnInit } from '@angular/core';
import { Tour } from '../../../core/models/tour.model';
import { TourService } from '../../../core/services/tour.service';
import { CommonModule } from '@angular/common';
import { TourFormComponent } from '../tour-form/tour-form.component';
import { TourDetailComponent } from '../tour-detail/tour-detail.component';
import { TourListComponent } from '../tour-list/tour-list.component';
import { SearchBarComponent } from '../../../shared/components/search-bar/search-bar.component';

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
    

    constructor(private tourService: TourService){}

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


    formSubmit(formData: Tour){
        const exists = this.tours.some(t => Number(t.id) === Number(formData.id));
        if(exists){
            this.updateTour(formData);
        }
        else{
          this.addTour(formData)
        }
    }

    //add new tour via tour service
    addTour(formData: Tour): void
    {
      console.log("add new tour")
      this.tourService.createTour(formData).subscribe({
        next: () => {
            this.hideForm();
            this.loadTours();
        }
      })
    }

    deleteTour(): void
    {
      if(this.selectedTour){
        this.tourService.deleteTour(this.selectedTour.id).subscribe({
          next: () => {
              this.selectedTour = null; // Reset selected tour after deletion
              this.loadTours();
          }
        })
      }
    }
    //nur show form statt uodate

    updateTour(formData: Tour): void
    {
        console.log("update tour")
        this.tourService.updateTour(formData).subscribe({
        next: () => {
            this.hideForm();
            this.loadTours();
            //eig tourdetails neu laden
            this.selectedTour = null;
        }
      })
    }
}