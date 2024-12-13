export interface Post {
  id: number;
  content: string;
  likes: number;
  user: Object &{
    id: number;
    username: string;
    email: string;
  };
  createdAt: Date;
  updatedAt: Date;
}
