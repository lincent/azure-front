import { Component } from '@angular/core';
import { CardComponent } from './card/card.component';

@Component({
  selector: 'app-root',
  imports: [CardComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  cards = [
    { id: 1, title: 'card ones title', content: 'this is the content of card one'},
    { id: 2, title: 'card twos title', content: 'this is the content of card two'}
  ]
}
