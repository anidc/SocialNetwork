import { Component } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../interfaces/post';
import { CommentsService } from '../../services/comments.service';
import { Comment } from '../../interfaces/comment';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { ToastrService } from 'ngx-toastr';
import { PostItem } from '../../interfaces/post-item';
import { CommentItem } from '../../interfaces/comment-item';

@Component({
  selector: 'app-home',
  standalone: false,

  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  postItems!: PostItem[];
  postForm!: FormGroup;
  editPostForm!: FormGroup;

  commentForm!: FormGroup;
  editCommentForm!: FormGroup;

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
    this.initEditPost();

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
            isEditing: false,
          } as PostItem)
      );
    });
  }

  initPublishPost() {
    this.postForm = this.formBuilder.group({
      content: ['', [Validators.required, Validators.maxLength(2560)]],
    });
  }

  initEditPost() {
    this.editPostForm = this.formBuilder.group({
      content: ['', [Validators.required, Validators.maxLength(2560)]],
    });
  }

  initPublishComment() {
    this.commentForm = this.formBuilder.group({
      text: ['', [Validators.required, Validators.maxLength(2560)]],
    });
  }

  initEditComment(comment: Comment) {
    this.editCommentForm = this.formBuilder.group({
      id: [comment.id],
      text: [comment.text, [Validators.required, Validators.maxLength(2560)]],
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
            .comments.push({
              comment: comment,
              isEditing: false,
            });
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

  deletePost(postId: number) {
    this.postService.deletePost(postId).subscribe({
      next: () => {
        this.getAllPosts();
        this.toastr.success('Post deleted successfully');
      },
      error: (error) => {
        this.toastr.error(error.error.toString());
      },
    });
  }

  editPost(postId: number) {
    this.postItems.forEach((postItem) => {
      if (postItem.post.id == postId) {
        postItem.isEditing = true;
        this.editPostForm.setValue({
          content: postItem.post.content,
        });
      } else {
        postItem.isEditing = false;
      }
    });
  }

  updatePost(postId: number) {
    const updatedContent = this.editPostForm.value.content;

    this.postService
      .updatePost({ content: updatedContent, id: postId } as Post)
      .subscribe({
        next: () => {
          const postItem = this.postItems.find((p) => p.post.id == postId);
          if (postItem) {
            postItem.post.content = updatedContent;
            postItem.post.updatedAt = new Date();
            postItem.isEditing = false;
          }
          this.toastr.success('Post updated successfully');
        },
        error: (error) => {
          this.toastr.error(error.error.toString());
        },
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
        postItem!.comments = newComments.map((c) => ({
          comment: c,
          isEditing: false,
        }));
      });
  }

  deleteComment(commentId: number) {
    this.commentsService.deleteComment(commentId).subscribe({
      next: () => {
        this.postItems.forEach((postItem) => {
          postItem.comments.forEach((comment) => {
            if (comment.comment.id == commentId) {
              postItem.comments.splice(postItem.comments.indexOf(comment), 1);
            }
          });
        });
        this.toastr.success('Comment deleted successfully');
      },
      error: (error) => {
        this.toastr.error(error.error.toString());
      },
    });
  }

  editComment(postId: number, commentId: number) {
    var comment = this.postItems
      .find((postItem) => postItem.post.id === postId)!
      .comments.find((comment) => comment.comment.id === commentId);

    this.postItems.forEach((postItem) => {
      postItem.comments.forEach((comment) => {
        if (comment.comment.id != commentId) {
          comment.isEditing = false;
        }
      });
    });

    this.initEditComment(comment!.comment);

    comment!.isEditing = !comment!.isEditing;
  }

  updateComment(commentItem: CommentItem) {
    var editFormValue = this.editCommentForm.value;

    this.commentsService.updateComment(editFormValue).subscribe({
      next: () => {
        commentItem.comment.text = editFormValue.text;
        commentItem.comment.updatedAt = new Date();
        commentItem.isEditing = false;

        this.toastr.success('Comment updated successfully');
      },
      error: (error) => {
        this.toastr.error(error.error.toString());
      },
    });
  }
}
