import { Component, OnInit, Inject } from '@angular/core';
import { AuthorizeService } from '../authorize.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { async } from 'rxjs/internal/scheduler/async';

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.css']
})
export class LoginMenuComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;
  public userBalance: number;
  private balanceInterval: any;

  constructor(private authorizeService: AuthorizeService,
    private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));

    // TODO: find a better way to update the balance,
    //        and also get it initially
    this.balanceInterval = setInterval(() => {
      if (this.isAuthenticated) {
        this.getBalance();
      }
    }, 1000);
  }

  getBalance(): void {
    this.httpClient.get(this.baseUrl + 'user/get').subscribe(result => {
      this.userBalance = result['user'].balance;
    }, error => {
      // eat un-auth errors
      if (error.status !== 401) {
        console.error(error)
      }
    });
  }
}
