import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SimpleLayoutComponent } from './layout/simple-layout/simple-layout.component';
import { DashboardLayoutComponent } from './layout/dashboard-layout/dashboard-layout.component';

import { IndexComponent } from './components/index/index.component';
import { RegisterComponent } from './components/register/register.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { WorkComponent } from './components/work/work.component';

import { TestComponent } from './components/test/test.component';

import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {
    path: '', component: SimpleLayoutComponent,
    children: [
      { path: '', component: IndexComponent },
      { path: 'register', component: RegisterComponent }
    ]
  },
  {
    path: '', component: DashboardLayoutComponent, canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'work', component: WorkComponent },
      { path: 'test', component: TestComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
