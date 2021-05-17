import { Component, OnInit } from '@angular/core';
import { User, UserService}  from '../../services/user.service';
import { ArenaService, Match } from '../../services/arena.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-arena',
  templateUrl: './arena.component.html',
  styleUrls: ['./arena.component.scss']
})
export class ArenaComponent implements OnInit {
  user$: Observable<User>;
  matches$: Observable<Match[]>;
  displayedColumns: string[] = ['id', 'name', 'createdDate', 'expiry'];
  expandedElement: Match | null;
  constructor(userService: UserService, private arenaService: ArenaService) {
    this.user$ = userService.user$;
    this.matches$ = this.arenaService.getMatches();
  }

  ngOnInit(): void {
  }

  refreshList() {
    this.matches$ = this.arenaService.getMatches();
  }

  addMatch() {
    this.arenaService.addMatch().subscribe();
  }
}
