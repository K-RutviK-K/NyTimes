import { Component } from '@angular/core';
import { ApiService } from '../../../core/api.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastService } from '../../../core/toast.service';

@Component({
  selector: 'app-home',
  imports: [CommonModule,FormsModule,ReactiveFormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  articles: any[] = [];
  apiForm: FormGroup;

  constructor(private apiService: ApiService,public toastService: ToastService,private fb: FormBuilder) {
    this.apiForm = this.fb.group({
      apiKey: [
        '', 
        [
          Validators.required, 
          Validators.minLength(32), 
          Validators.maxLength(32)
        ]
      ]
    });
  }

  ngOnInit(): void {
    this.apiService.getAllStories().subscribe({
      next: (response) => {
        this.articles = response
        this.toastService.showToast('News fetched successfully!', 'success');

      },
      error: (error) => {
        debugger
        this.toastService.showToast(error?.error, 'danger');
      }
    });
  }

  submit()
  {
    if (this.apiForm.invalid) {
      return;
    }
    const apiKey = this.apiForm.value.apiKey;
    
    this.apiService.getTopStories(apiKey).subscribe({
      next: (response) => {
        this.articles = response['results'];
        this.toastService.showToast('News fetched successfully!', 'success');

      },
      error: (error) => {
        debugger
        this.toastService.showToast(error?.error, 'danger');
      }
    });
  }

  getErrors(field: string): string[] {
    const control = this.apiForm.get(field);
    if (!control || !control.errors) return [];
  
    const errorMessages: { [key: string]: string } = {
      required: 'API Key is required.',
      minlength: 'API Key must be at least 32 characters.',
      maxlength: 'API Key cannot be more than 32 characters.'
    };
  
    return Object.keys(control.errors).map(err => errorMessages[err]);
  }
  
}
