export interface Comment {
  id: number;
  content: string;
  user: Object & {
    id: number;
    username: string;
    email: string;
  };
  createdAt: Date;
  updatedAt: Date;
}
