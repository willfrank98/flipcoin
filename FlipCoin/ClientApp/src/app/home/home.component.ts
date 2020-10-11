import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  user: any;

  queueItems: any;
  newAmount: number;
  queueRefresh: any;

  incomingChallenge: any;
  outgoingChallenge: any;
  challengeRefresh: any;

  coinResult: number;

  constructor(private readonly httpClient: HttpClient,
    @Inject('BASE_URL') private readonly baseUrl: string) {}

  ngOnInit(): void {
    // get user
    this.httpClient.get(this.baseUrl + 'user/get').subscribe(result => {
      this.user = result['user'];
    }, error => {
      // eat un-auth errors
      if (error.status !== 401) {
        console.error(error)
      }
      else {
        console.log(error);
      }
    });

    // set queue refresh timer
    this.queueRefresh = setInterval(() => {
      this.getQueue();
    }, 1000);
  }

  getQueue(): void {
    this.httpClient.get(this.baseUrl + 'queue/get').subscribe(result => {
      this.queueItems = result;
    }, error => console.error(error));
  }

  addQueueItem(): void {
    if (this.newAmount > this.user.balance) {
      return;
    }

    const model = {
      userId: this.user.id,
      amount: this.newAmount
    }

    this.httpClient.post(this.baseUrl + 'queue/add', model).subscribe(result => {
      this.queueItems = result;

      this.getQueue();

      // set challenges refresh rate
      //this.challengeRefresh = setInterval(() => {
      //  this.refreshChallenges();
      //}, 500);
    }, error => console.error(error));
  }

  deleteQueueItem(id: number): void {
    this.httpClient.get(this.baseUrl + 'queue/remove/' + id).subscribe(result => {
      console.log(result);

      this.getQueue();
    }, error => console.error(error));
  }

  challenge(id: number): void {
    this.httpClient.get(this.baseUrl + 'challenge/add/' + id).subscribe(result => {
      this.outgoingChallenge = result['challenge'];

      // faster refresh rate to check for challenge accept
      //clearInterval(this.challengeRefresh);
      //this.challengeRefresh = setInterval(() => {
      //  this.refreshChallenges();
      //}, 100);
    }, error => console.error(error));
  }

  refreshChallenges(): void {
    this.httpClient.get(this.baseUrl + 'challenge/check').subscribe(result => {
      this.incomingChallenge = result['incomingChallenge'];
      this.outgoingChallenge = result['outgoingChallenge'];

      if (this.outgoingChallenge && this.outgoingChallenge.result) {
        this.coinResult = this.outgoingChallenge.result;

        clearInterval(this.challengeRefresh);
      }

    }, error => console.error(error));
  }

  acceptChallenge(): void {
    this.httpClient.get(this.baseUrl + 'challenge/accept/' + this.incomingChallenge.id).subscribe(result => {
      this.coinResult = result['challenge'].result;
    }, error => console.error(error));
  }
}
