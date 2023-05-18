import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
//these are decorators.
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

//just like we have dependency injection in dotnet file we also have in angular i.e, we have to inject http module.

//constructor is too early to fetch data from an api so lifecycle process is implemented(OnInit).
export class AppComponent implements OnInit {
//title can br written after constructor or before.
  title = 'DatingApp'; //title is of type string.
  users:any;  //user is of type any.

  constructor(private http: HttpClient){}
  ngOnInit(): void {
    this.http.get("https://localhost:5000/api/users").subscribe({
      next:response => this.users = response,
      error: error=>console.log(error),
      complete:() =>console.log("request completed")
    })
  }



  

}
