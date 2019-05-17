import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatToolbarModule, MatSidenavModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './layout/app.component';
import { RegisterComponent } from './components/register/register.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { AlertComponent } from './components/alert/alert.component';
import { TestComponent } from './components/test/test.component';
import { LoginComponent } from './components/login/login.component';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardLayoutComponent } from './layout/dashboard-layout/dashboard-layout.component';
import { SimpleLayoutComponent } from './layout/simple-layout/simple-layout.component';
import { IndexComponent } from './index/index.component';
import { SidenavComponent } from './layout/sidenav/sidenav.component';
import { MiddleContainerComponent } from './layout/middle-container/middle-container.component';
import { ContentComponent } from './layout/content/content.component';
// import { CvComponent } from './cv/cv.component';
// import { CvEnglishComponent } from './cv-english/cv-english.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    HeaderComponent,
    FooterComponent,
    AlertComponent,
    TestComponent,
    LoginComponent,
    DashboardComponent,
    DashboardLayoutComponent,
    SimpleLayoutComponent,
    IndexComponent,
    SidenavComponent,
    MiddleContainerComponent,
    ContentComponent
    // CvComponent,
    // CvEnglishComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatSidenavModule,
    BrowserAnimationsModule,
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    HttpClientModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
