export interface Comment {
  id: number;
  text: string;
  user: Object & {
    id: number;
    username: string;
    email: string;
  };
  createdAt: Date;
  updatedAt: Date;
}
