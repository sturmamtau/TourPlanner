export enum Difficulty {
  "Easy",
  "Medium",
  "Hard"
}

export interface TourLog {
  id: number;
  dateTime: string; //bruahct dann umwandlung in date
  comment?: string;
  difficulty: Difficulty;
  totalDistance: number;
  totalTime: number;
  rating: number;
  tourId: number;
}