import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
members: Member[] = [];


constructor(private memberService: MembersService) {

}

ngOnInit(): void{
  this.loadMembers();
}

loadMembers(){
  //this.memberService.getMembers returns observable so we need to subscribe
  this.memberService.getMembers().subscribe({
    //accessing to observer objects
    next:members=> this.members = members
    //in order to load members we use OnInit
  })
  
}
}
