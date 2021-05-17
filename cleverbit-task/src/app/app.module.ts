import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { ScoreBoardComponent } from './components/score-board/score-board.component';
import { ArenaComponent } from './components/arena/arena.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ScoreBoardComponent,
    ArenaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
