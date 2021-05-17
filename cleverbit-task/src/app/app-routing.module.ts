import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { ScoreBoardComponent } from './components/score-board/score-board.component';
import { ArenaComponent } from './components/arena/arena.component';
import { AuthGuard } from './services/auth-guard';
import { MatchComponent}  from './components/arena/match/match.component';
import { ScoresComponent } from './components/score-board/scores/scores.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', component: LoginComponent },
  { path: 'scoreboard', component: ScoreBoardComponent },
  { path: 'scoreboard/:id', component: ScoresComponent },
  { path: 'arena', component: ArenaComponent, canActivate: [AuthGuard]},
  { path: 'arena/:id', component: MatchComponent, canActivate: [AuthGuard] }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
