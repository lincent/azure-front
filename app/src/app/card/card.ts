import { Component, input, signal } from '@angular/core';
import { MatCard, MatCardContent, MatCardHeader, MatCardTitle } from '@angular/material/card';

@Component({
  selector: 'app-card',
  imports: [MatCard, MatCardHeader, MatCardTitle, MatCardContent],
  templateUrl: './card.html',
  styleUrl: './card.css'
})
export class Card {
  title = input('title')
  content = input('content')
}
