import { TourLog } from './tourLog.model';

export enum TransportType {
  Run = 'Run',
  Bike = 'Bike',
  Walk = 'Walk',
  Vacation = 'Vacation'
}

export interface Tour {
  id: number;
  name: string;
  description?: string;
  from: string;
  to: string;
  transportType: TransportType;
  tourDistance: number;
  estimatedTime: number;
  imagePath?: string;
  popularity: number;
  isChildFriendly: boolean;
  userId: number;
  tourLogs?: TourLog[];
}
