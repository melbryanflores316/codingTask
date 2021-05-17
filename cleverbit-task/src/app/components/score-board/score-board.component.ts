import { Component, OnInit } from '@angular/core';
import { User, UserService } from '../../services/user.service';
import { ArenaService, Match } from '../../services/arena.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-score-board',
  templateUrl: './score-board.component.html',
  styleUrls: ['./score-board.component.scss']
})
export class ScoreBoardComponent implements OnInit {
  displayedColumns = ['id','name','createdDate','expiry'];
  user$ ;
  matches$;
  expandedElement: Match | null;

  constructor(userService: UserService, private arenaService: ArenaService) {
    this.user$ = userService.user$;
    this.matches$ = arenaService.getMatches();
  }

  ngOnInit(): void {
  }

  refreshList() {
    this.matches$ = this.arenaService.getMatches();
  }
}
