import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member: Member | undefined;

  //I want ot get member from API so i have created a constructor
  
  constructor(private memberService: MembersService, private route: ActivatedRoute) {}

  ngOnInit(): void{
    this.loadMember();
  }

  loadMember() {
    //the router is not knowing if username is an actual route parameter.
    const username = this.route.snapshot.paramMap.get('username');
    if(!username) return ;
    this.memberService.getMember(username).subscribe({
      next: member=> this.member = member
    })
  }

}
