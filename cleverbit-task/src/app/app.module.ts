import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
// services
import { UserService } from './services/user.service';
import { BasicAuthInterceptor } from './services/basic-auth-interceptor.service';
import { ErrorInterceptor } from './services/error-interceptor.service';
// components
import { LoginComponent } from './components/login/login.component';
import { ScoreBoardComponent } from './components/score-board/score-board.component';
import { ArenaComponent } from './components/arena/arena.component';
import { ArenaService } from './services/arena.service';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatchComponent } from './components/arena/match/match.component';
import { ScoresComponent } from './components/score-board/scores/scores.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ScoreBoardComponent,
    ArenaComponent,
    MatchComponent,
    ScoresComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NoopAnimationsModule,
    MatTableModule,
    NgbModule
  ],
  providers: [
    UserService,
    ArenaService,
    { provide: HTTP_INTERCEPTORS, useClass: BasicAuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
