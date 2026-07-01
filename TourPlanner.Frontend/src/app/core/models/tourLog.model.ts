export enum Difficulty {
  Easy = 1,
  Medium = 2,
  Hard = 3
}

export interface TourLog {
  id: number;
  dateTime: string; //bruahct dann umwandlung in date
  comment: string;
  difficulty: Difficulty;
  totalDistance: number;
  totalTime: number;
  rating: number;
  tourId: number;
}