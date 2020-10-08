import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  user: any;

  queueItems: any;
  newAmount: string;

  incomingChallenge: any;
  outgoingChallenge: any;

  coinResult: number;

  private readonly _httpClient: HttpClient;
  private readonly _baseUrl: string;
  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._httpClient = httpClient;
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
    // get user
    this._httpClient.get(this._baseUrl + 'user/get').subscribe(result => {
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

    // get queue
    this.getQueue();

    // get challenges
    this.refreshChallenges();
  }

  getQueue(): void {
    this._httpClient.get(this._baseUrl + 'queue/get').subscribe(result => {
      this.queueItems = result;
    }, error => console.error(error));
  }

  addQueueItem(): void {
    const model = {
      userId: this.user.id,
      amount: this.newAmount
    }

    this._httpClient.post(this._baseUrl + 'queue/add', model).subscribe(result => {
      this.queueItems = result;

      this.getQueue();
    }, error => console.error(error));
  }

  deleteQueueItem(id: number): void {
    this._httpClient.get(this._baseUrl + 'queue/remove/' + id).subscribe(result => {
      console.log(result);

      this.getQueue();
    }, error => console.error(error));
  }

  challenge(id: number): void {
    this._httpClient.get(this._baseUrl + 'challenge/add/' + id).subscribe(result => {
      this.outgoingChallenge = result['challenge'];
    }, error => console.error(error));
  }

  refreshChallenges(): void {
    this._httpClient.get(this._baseUrl + 'challenge/check').subscribe(result => {
      this.incomingChallenge = result['incomingChallenge'];
      this.outgoingChallenge = result['outgoingChallenge'];

      if (!this.outgoingChallenge.inProgress) {
        this.coinResult = this.outgoingChallenge.result;
      }

    }, error => console.error(error));
  }

  acceptChallenge(): void {
    this._httpClient.get(this._baseUrl + 'challenge/accept/' + this.incomingChallenge.id).subscribe(result => {
      this.coinResult = result['challenge'].result;
    }, error => console.error(error));
  }
}
