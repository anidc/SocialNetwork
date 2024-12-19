import { Comment } from './comment';
import { Post } from './post';

export interface PostItem {
  post: Post;
  isCommentListOpen: boolean;
  comments: Comment[];
}
