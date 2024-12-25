import { CommentItem } from './comment-item';
import { Post } from './post';

export interface PostItem {
  post: Post;
  isCommentListOpen: boolean;
  comments: CommentItem[];
  isEditing?: boolean;
}
