import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatToolbarModule, MatSidenavModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './layout/app.component';
import { DashboardLayoutComponent } from './layout/dashboard-layout/dashboard-layout.component';
import { SimpleLayoutComponent } from './layout/simple-layout/simple-layout.component';

import { HeaderComponent } from './layout/components/header/header.component';
import { FooterComponent } from './layout/components/footer/footer.component';
import { SidenavComponent } from './layout/components/sidenav/sidenav.component';
import { ContentComponent } from './layout/components/content/content.component';
import { BoxComponent } from './layout/components/box/box.component';
import { AlertComponent } from './layout/components/alert/alert.component';

import { TestComponent } from './components/test/test.component';
import { IndexComponent } from './components/index/index.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { WorkComponent } from './components/work/work.component';
import { WorkdayComponent } from './components/work/workday/workday.component';
import { WorkhistoryComponent } from './components/work/workhistory/workhistory.component';
import { WorksettingsComponent } from './components/work/worksettings/worksettings.component';

import { JwtInterceptor } from './helpers/jwt.interceptor';

// import { CvComponent } from './cv/cv.component';
import { CvEnglishComponent } from './cv-english/cv-english.component';

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
    ContentComponent,
    WorkComponent,
    WorkdayComponent,
    WorkhistoryComponent,
    WorksettingsComponent,
    BoxComponent,
    // CvComponent,
    CvEnglishComponent
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
