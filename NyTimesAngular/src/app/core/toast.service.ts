import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  toastMessage: string = '';
  toastClass: string = '';

  constructor() {}

  showToast(message: string, type: 'success' | 'danger' | 'warning') {
    this.toastMessage = message;
    this.toastClass = `alert alert-${type} alert-dismissible fade show`;

    setTimeout(() => this.clearToast(), 3000);
  }

  clearToast() {
    this.toastMessage = '';
    this.toastClass = '';
  }
}
