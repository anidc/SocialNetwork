import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment.development";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor (private http: HttpClient) { }

    login (username: string, password: string) { 
        return this.http.post(`${environment.serverUrl}/api/login`, { username, password });
    }
}