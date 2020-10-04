import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';

//import { QueueService } from './queue.service';

@Component({
  selector: 'queue',
  templateUrl: './queue.component.html'
})
export class QueueComponent implements OnInit {
  @Input() user: any;

  queueItems: any;
  newAmount = '';

  private readonly _httpClient: HttpClient;
  private readonly _baseUrl: string;
  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._httpClient = httpClient;
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
    console.log(this.user);
    this.getQueue();
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

  }
}
