import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment'; 

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) {}

  getWeather(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}WeatherForecast`, {
      headers: { 'accept': 'text/plain' }
    });
  }

  getTopStories(apiKey: string):Observable<any> {
    return this.http.post(`${this.apiUrl}Article/PostByKey`,{key:apiKey});
  }
  getAllStories():Observable<any> {
    return this.http.get(`${this.apiUrl}Article`);
  }
}
