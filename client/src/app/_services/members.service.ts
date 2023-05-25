import { HttpClient, HttpHeaderResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  //I should use environment here but im using hard code here
  //baseUrl=environment.apiUrl;

  baseUrl = "http://localhost:5001/api/";

  constructor(private http: HttpClient) { }

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users')

    //we also need to send token with this. so we use gethttpoptions
  }

  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'users/')
  }

  //I have made a interceptor for tokens so that i dont have to do send again and again

}