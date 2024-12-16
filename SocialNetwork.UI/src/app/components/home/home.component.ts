import { Component } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../interfaces/post';
import { CommentsService } from '../../services/comments.service';
import { Comment } from '../../interfaces/comment';

@Component({
  selector: 'app-home',
  standalone: false,

  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  posts!: Post[];
  comments!: Comment[];
  activePostId: number | null = null;

  constructor(
    private readonly postService: PostsService,
    private readonly commentsService: CommentsService
  ) {
    this.getAllPosts();
  }

  getAllPosts() {
    this.postService.getAllPosts().subscribe((posts) => {
      this.posts = posts;
      console.log(this.posts);
    });
  }

  likePost(postId: number) {
    this.postService.toggleLike(postId).subscribe((response) => {
      var post = this.posts.find((post) => post.id === postId);
      post!.isLikedByCurrentUser = !post!.isLikedByCurrentUser;

      if (post!.isLikedByCurrentUser) {
        post!.likes += 1;
      } else {
        post!.likes -= 1;
      }

      console.log(response);
    });
  }

  showComments(postId: number) {
    if (this.activePostId === postId) {
      this.activePostId = null;
      this.comments = [];
    } else {
      this.activePostId = postId;
      this.commentsService
        .getAllCommentsByPost(postId)
        .subscribe((newComments: Comment[]) => {
          this.comments = newComments;
          console.log(this.comments);
        });
    }
  }
}
