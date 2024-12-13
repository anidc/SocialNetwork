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
  constructor(private readonly postService: PostsService, private readonly commentsService: CommentsService) {
    this.getAllPosts();
  }

  getAllPosts() {
    this.postService.getAllPosts().subscribe((posts) => {
      this.posts = posts;
      console.log(this.posts);
    });
  }

  likePost(postId: number) {
    this.postService.getPostById(postId).subscribe((post: Post) => {
      console.log(post)
      post.likes += 1;
      this.postService.updatePost(post).subscribe();
    });
  }

  showComments(postId: number) {
    this.commentsService.getAllCommentsByPost(postId).subscribe((comments) => {
      console.log(comments);
    })
  }
}
