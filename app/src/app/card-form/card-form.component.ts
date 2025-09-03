import { Component, signal } from '@angular/core';
import { FormBuilder, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-card-form',
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule],
  templateUrl: './card-form.component.html',
  styleUrls: ['./card-form.component.css']
})
export class CardFormComponent {
  cardForm: FormGroup;
  submitted = signal(false);
  success = signal(false);
  error = signal('');

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.cardForm = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required]
    });
  }

  onSubmit() {
    this.submitted.set(true);
    this.success.set(false);
    this.error.set('');
    if (this.cardForm.invalid) {
      return;
    }
    this.http.post('/api/cards', this.cardForm.value).subscribe({
      next: () => {
        this.success.set(true);
        this.cardForm.reset();
        this.submitted.set(false);
      },
      error: () => {
        this.error.set('Failed to submit card.');
      }
    });
  }
}
