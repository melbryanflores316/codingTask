import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ArenaService {
  private url = `${environment.apiUrl}/arenas`;
  constructor(private httpClient: HttpClient) { }

  getMatches() {
    return this.httpClient.get<any>(`${this.url}/match` )
      .pipe(map(matches => matches));
  }

  getMatch(id) {
    return this.httpClient.get<any>(`${this.url}/match/${id}` );
  }

  addMatch() {
    return this.httpClient.post<any>(`${this.url}/match`, {});
  }

  sendEntry(entry) {
    return this.httpClient.post<any>(`${this.url}/entry`, entry);
  }

  getScoreBoard(id) {
    return this.httpClient.get<any>(`${this.url}/scores/${id}`);
  }

  getScoreBoards(matchId) {
    return this.httpClient.get<any>(`${this.url}/scoreboard/${matchId}`);
  }


}

export class Match {
  id: number;
  name: string;
  createdDate: Date;
  expiry: Date;
}
