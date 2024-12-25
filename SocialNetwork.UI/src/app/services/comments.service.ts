import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Comment } from '../interfaces/comment';

@Injectable({
  providedIn: 'root',
})
export class CommentsService {
  constructor(private http: HttpClient) {}

  getAllCommentsByPost(postId: number): Observable<Comment[]> {
    return this.http.get<Comment[]>(
      `${environment.serverUrl}/api/comment/${postId}`
    );
  }

  createComment(postId: number, comment: Comment): Observable<Comment> {
    return this.http.post<Comment>(
      `${environment.serverUrl}/api/comment/${postId}`,
      comment
    );
  }

  updateComment(comment: Comment): Observable<Comment> {
    return this.http.put<Comment>(
      `${environment.serverUrl}/api/comment/${comment.id}`,
      comment
    );
  }

  deleteComment(commentId: number): Observable<void> {
    return this.http.delete<void>(
      `${environment.serverUrl}/api/comment/${commentId}`
    );
  }
}
