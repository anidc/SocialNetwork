import { Comment } from './comment';

export interface CommentItem {
  comment: Comment;
  isEditing?: boolean;
}
