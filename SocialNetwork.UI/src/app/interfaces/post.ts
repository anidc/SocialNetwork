import { User } from "./user";

export interface Post {
  id: number;
  content: string;
  likes: number;
  user: User;
  isLikedByCurrentUser : boolean;
  createdAt: Date;
  updatedAt: Date;
}
