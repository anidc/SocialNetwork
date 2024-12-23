import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Post } from '../interfaces/post';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PostsService {
  constructor(private http: HttpClient) {}

  getAllPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(`${environment.serverUrl}/api/post`);
  }

  getPostById(id: number): Observable<Post> {
    return this.http.get<Post>(`${environment.serverUrl}/api/post/${id}`);
  }

  updatePost(post: Post): Observable<Post> {
    return this.http.put<Post>(
      `${environment.serverUrl}/api/post/${post.id}`,
      post
    );
  }

  createPost(post: Post): Observable<Post> {
    return this.http.post<Post>(`${environment.serverUrl}/api/post`, post);
  }

  toggleLike(postId: number): Observable<boolean> {
    return this.http.post<boolean>(
      `${environment.serverUrl}/api/post/${postId}/like`,
      {}
    );
  }

  deletePost(postId: number): Observable<void> {
    return this.http.delete<void>(
      `${environment.serverUrl}/api/post/${postId}`
    );
  }
}
