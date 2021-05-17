import { Component, OnInit } from '@angular/core';
import { ArenaService, Match } from '../../../services/arena.service';
import { UserService } from '../../../services/user.service';
import { Observable}  from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-scores',
  templateUrl: './scores.component.html',
  styleUrls: ['./scores.component.scss']
})
export class ScoresComponent implements OnInit {
  displayedColumns = ['id','userName','matchName','score'];

  scores$: Observable<any>;
  result = {
    score: 0,
    matchId: 0,
    userId: 0,
    id: 0
  };
  private sub: any;
  id: number;
  constructor(private route: ActivatedRoute, private arenaService: ArenaService) {}

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = +params[`id`];
      this.scores$ = this.arenaService.getScoreBoards(this.id);
    });

  }

  refreshList() {
    this.scores$ = this.arenaService.getScoreBoards(this.id);
  }
}
