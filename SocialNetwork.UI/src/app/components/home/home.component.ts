import { Component } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../interfaces/post';
import { CommentsService } from '../../services/comments.service';
import { Comment } from '../../interfaces/comment';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  standalone: false,

  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  posts!: Post[];
  postForm!: FormGroup;
  comments!: Comment[];
  activePostId: number | null = null;
  isAuthenticated = false;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly postService: PostsService,
    private readonly commentsService: CommentsService,
    public accountService: AccountService,
    private toastr: ToastrService
  ) {
    this.getAllPosts();
    this.initPublishPost();
    this.isAuthenticated = this.accountService.isUserAuthenticated();
  }

  getAllPosts() {
    this.postService.getAllPosts().subscribe((posts) => {
      this.posts = posts;
    });
  }

  initPublishPost() {
    this.postForm = this.formBuilder.group({
      content: ['', [Validators.required, Validators.maxLength(2560)]],
    });
  }

  publishPost() {
    if (this.postForm.invalid) {
      console.log('Invalid post');
      return;
    }

    const newPost: any = {
      content: this.postForm.value.content,
      likes: 0,
    };

    this.postService.createPost(newPost).subscribe({
      next: (response) => {
        this.getAllPosts();
        this.postForm.reset();
      },
      error: (error) => {
        this.toastr.error(error.error.toString());
      },
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
        });
    }
  }
}
