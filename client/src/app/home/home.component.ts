import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
  registerMode = false;
  users: any;

//since we put here getusers we have to iject that inside the constructor.
  constructor(private http: HttpClient) {

    
  }

  ngOnInit(): void {
    this.getUsers();
    
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  
  getUsers(){
    this.http.get("https://localhost:5000/api/users").subscribe({
      next:response => this.users = response,
      error: error=>console.log(error),
      complete:() =>console.log("request completed")
    })
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

}