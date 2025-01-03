import { User } from './user';

export interface Comment {
  id: number;
  text: string;
  user: User;
  createdAt: Date;
  updatedAt: Date;
}
