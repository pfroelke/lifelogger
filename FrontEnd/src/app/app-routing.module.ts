import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { TestComponent } from './components/test/test.component';

const routes: Routes = [
  { path: 'register', component: RegisterComponent},
  { path: 'test', component: TestComponent},
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
