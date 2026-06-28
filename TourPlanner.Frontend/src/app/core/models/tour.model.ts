import { TourLog } from './tourLog.model';

export enum TransportType {
  Run = 'Run',
  Bike = 'Bike',
  Walk = 'Walk',
  Vacation = 'Vacation'
}

// for list
export interface TourList {
  id: number;
  name: string;
  from: string;
  to: string;
  transportType: string;
}

// for details
export interface TourDetails {
  id: number;
  name: string;
  description: string;
  from: string;
  to: string;
  transportType: string;
  tourDistance: number;
  estimatedTime: number;
  imageUrl?: string;
  popularity: number;
  isChildFriendly: boolean;
  tourLogs: TourLog[];
}

// for tour form
export interface CreateTour {
  name: string;
  description?: string;
  from: string;
  to: string;
  transportType: string;
  tourDistance: number;
  estimatedTime: number;
}
