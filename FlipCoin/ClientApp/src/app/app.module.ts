import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

// always-present components
import { AppComponent } from './app.component';
import { ChallengeComponent } from './challenge/challenge.component';
import { MenubarComponent } from './menubar/menubar.component'

// routable pages
import { HomeComponent } from './home/home.component';
import { StatsComponent } from './stats/stats.component';

// sub components
import { CoinComponent } from './home/coin/coin.component';
import { QueueComponent } from './home/queue/queue.component';

// API stuff
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
//import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

@NgModule({
  declarations: [
    // always-present components
    AppComponent,
    ChallengeComponent,
    MenubarComponent,
    // routable components
    HomeComponent,
    StatsComponent,
    // sub components
    CoinComponent,
    QueueComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }
      //{ path: 'counter', component: CounterComponent },
      //{ path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
