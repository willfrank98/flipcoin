import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Winwheel } from 'winwheel';

//import { QueueService } from './queue.service';

@Component({
  selector: 'coin',
  templateUrl: './coin.component.html'
})
export class CoinComponent implements OnInit {
  @Input()
  result: number;

  private wheel: Winwheel;

  constructor(private readonly _httpClient: HttpClient,
    private readonly _baseUrl: string) { }

  ngOnInit(): void {
    this.wheel = new Winwheel({
      'outerRadius': 200,
      'textFontSize': 24,
      'textOrientation': 'vertical',
      'textAlignment': 'outer',
      'numSegments': 2,
      'segments': [
        { 'fillStyle': 'red', 'text': 'red' },
        { 'fillStyle': 'blue', 'text': 'blue' }
      ]
    })
  }
}
