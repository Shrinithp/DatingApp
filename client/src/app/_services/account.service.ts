import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, pipe } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = "https://localhost:5000/api/";
  private currentUserSource = new BehaviorSubject<User| null>(null);
  // $ signifyies that its an observable.
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient){}

  login(model: any){
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User)=>{
        const user = response;
        if(user){
          localStorage.setItem('user', JSON.stringify(user))
          this.currentUserSource.next(user);
        }
      })
    )
  }
  //pipe which gives us access to rxjs operator so that we can transform or do something with these observables
  //before the component subscribes to it.
  register(model:any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
    map(user=>{
      if(user){
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUserSource.next(user);
      }
      //if we want to see if the user has registered in console use the below commented code.
      //return user;
    })
    )
  }

  setCurrentUser(user:User){
    this.currentUserSource.next(user);
  }


logout(){
  localStorage.removeItem('user');
  this.currentUserSource.next(null);
}
}