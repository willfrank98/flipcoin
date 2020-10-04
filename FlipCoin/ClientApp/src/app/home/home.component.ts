import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public user: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get(baseUrl + 'user/get').subscribe(result => {
      this.user = result;
    }, error => {
        // eat un-auth errors
        if (error.status !== 401) {
          console.error(error)
        }
        else {
          console.log(error);
        }
    });
  }
}
