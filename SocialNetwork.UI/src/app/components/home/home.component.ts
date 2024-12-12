import { Component } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../interfaces/post';

@Component({
  selector: 'app-home',
  standalone: false,

  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  posts!: Post[];
  constructor(private readonly postService: PostsService) {
    this.getAllPosts();
  }

  getAllPosts() {
    this.postService.getAllPosts().subscribe((posts) => {
      this.posts = posts;
      console.log(this.posts);
    });
  }
}
