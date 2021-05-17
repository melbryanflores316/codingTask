import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { config, Observable } from 'rxjs';
import { ArenaService, Match } from '../../../services/arena.service';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.scss']
})
export class MatchComponent implements OnInit {

  match$: Observable<any>;
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
      this.match$ = this.arenaService.getMatch(this.id);
      this.arenaService.getScoreBoard(this.id).subscribe(s => {
        if (s) {
          this.result = s;
        }
      });
    });

  }
  playNow() {
    const randomNumber = Math.round(Math.random() * 100);
    this.result.matchId = this.id;
    this.result.score = randomNumber;
    this.arenaService.sendEntry({...this.result}).subscribe(s => this.match$.pipe(map(a => a.canPlay = false)));
  }
}
