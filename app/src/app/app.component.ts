import { Component, inject, OnInit } from '@angular/core';
import { CardComponent } from './card/card.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CardFormComponent } from './card-form/card-form.component';

@Component({
  selector: 'app-root',
  imports: [CardComponent, HttpClientModule, CardFormComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  cards: any = []
  
  httpClient = inject(HttpClient)

  ngOnInit(): void {
    this.httpClient.get('/api/cards').subscribe(resp => this.cards = resp);
  }
}
