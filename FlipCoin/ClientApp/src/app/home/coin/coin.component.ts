import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

//import { QueueService } from './queue.service';

@Component({
  selector: 'coin',
  templateUrl: './coin.component.html'
})
export class CoinComponent implements OnInit {
  incomingChallenge: any;
  outgoingChallenge: any;

  private readonly _httpClient: HttpClient;
  private readonly _baseUrl: string;
  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._httpClient = httpClient;
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
  }

  //refresh(): void {
  //  this._httpClient.get(this._baseUrl + 'challenge/get').subscribe(result => {
  //    this.incomingChallenge = result['incomingChallenge'];
  //    this.outgoingChallenge = result['outgoingChallenge'];
  //  }, error => console.error(error));
  //}

  //accept(): void {
  //  this._httpClient.get(this._baseUrl + 'challenge/accept/' + this.incomingChallenge.id).subscribe(result => {

  //  }, error => console.error(error));
  //}
}
