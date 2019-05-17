import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { TestComponent } from './components/test/test.component';
import { AuthGuard } from './guards/auth.guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardLayoutComponent } from './layout/dashboard-layout/dashboard-layout.component';
import { SimpleLayoutComponent } from './layout/simple-layout/simple-layout.component';
import { IndexComponent } from './index/index.component';
// import { CvComponent } from './cv/cv.component';
// import { CvEnglishComponent } from './cv-english/cv-english.component';

const routes: Routes = [
  {
    path: '', component: SimpleLayoutComponent,
    children: [
      { path: '', component: IndexComponent },
      { path: 'register', component: RegisterComponent }
    ]
  },
  {
    path: '', component: DashboardLayoutComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
      { path: 'test', component: TestComponent }
    ]
  },
  // { path: 'cv', component: CvComponent },
  // { path: 'cveng', component: CvEnglishComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
