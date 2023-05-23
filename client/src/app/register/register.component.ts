import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  //we are going to get something from parent component(home) so we are using decorators
  @Output() cancelRegister = new EventEmitter();
  model: any ={}


  
  constructor(private accountService: AccountService) {
    
  }

  ngOnInit(): void {
    
  }

  register(){
    this.accountService.register(this.model).subscribe({
      next:() =>{
        this.cancel();
      },
      error: error=> {alert(error.error),
      console.log(error)
      }
    })
  }

  cancel(){
   this.cancelRegister.emit(false);
  }

}
