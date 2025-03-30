import { Routes } from '@angular/router';

export const routes: Routes = [
    // { path: '', component: HomeComponent },
    { path: '', loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule) }
  ];
  