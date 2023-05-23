import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

//takes input inside the form and prints in the console.
export class NavComponent implements OnInit{
  model: any = {};



  constructor(public accountService: AccountService) {
    
  }
  ngOnInit(): void{
    

  }

  // getCurrentUser(){
  //   this.accountService.currentUser$.subscribe({
  //     next: user =>this.loggedIn =!!user,
  //     error: error =>console.log(error)
  //   })
  // }

  login(){
    //subscribe says what to do next
    //netflix monthly subscription example .
    this.accountService.login(this.model).subscribe({
    next: response => {
      console.log(response);
      // this.loggedIn = true;
    },
    error: error =>console.log(error)
  })
  }
  logout(){
    this.accountService.logout();
    // this.loggedIn=false;
    
  }
}
