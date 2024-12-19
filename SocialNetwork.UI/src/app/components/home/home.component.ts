import { Component } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../interfaces/post';
import { CommentsService } from '../../services/comments.service';
import { Comment } from '../../interfaces/comment';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { ToastrService } from 'ngx-toastr';
import { PostItem } from '../../interfaces/post-item';

@Component({
  selector: 'app-home',
  standalone: false,

  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  postItems!: PostItem[];
  postForm!: FormGroup;
  commentForm!: FormGroup;
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
    this.initPublishComment();
    this.isAuthenticated = this.accountService.isUserAuthenticated();
  }

  getAllPosts() {
    this.postService.getAllPosts().subscribe((posts) => {
      this.postItems = posts.map(
        (post) =>
          ({
            post: post,
            isCommentListOpen: false,
            comments: [],
          } as PostItem)
      );
    });
  }

  initPublishPost() {
    this.postForm = this.formBuilder.group({
      content: ['', [Validators.required, Validators.maxLength(2560)]],
    });
  }

  initPublishComment() {
    this.commentForm = this.formBuilder.group({
      text: ['', [Validators.required, Validators.maxLength(2560)]],
    });
  }

  publishComment(postId: number) {
    if (this.commentForm.invalid) {
      console.log('Invalid comment');
      return;
    }

    this.commentsService
      .createComment(postId, this.commentForm.value)
      .subscribe({
        next: (comment) => {
          this.postItems
            .find((postItem) => postItem.post.id === postId)!
            .comments.push(comment);
          this.commentForm.reset();
          this.showComments(postId);
          this.showComments(postId);
        },
        error: (error) => {
          this.toastr.error(error.error.toString());
        },
      });
  }
  publishPost() {
    if (this.postForm.invalid) {
      console.log('Invalid post');
      return;
    }

    const newPost: any = {
      content: this.postForm.value.content,
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
      var post = this.postItems.find(
        (postItem) => postItem.post.id === postId
      )?.post;

      post!.isLikedByCurrentUser = !post!.isLikedByCurrentUser;

      if (post!.isLikedByCurrentUser) {
        post!.likes += 1;
      } else {
        post!.likes -= 1;
      }
    });
  }

  showComments(postId: number) {
    var postItem = this.postItems.find(
      (postItem) => postItem.post.id === postId
    );

    postItem!.isCommentListOpen = !postItem!.isCommentListOpen;

    this.commentsService
      .getAllCommentsByPost(postId)
      .subscribe((newComments: Comment[]) => {
        postItem!.comments = newComments;
      });
  }
}
