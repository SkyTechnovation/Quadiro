import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './../../src/app/component/app.component';

import { LoginService } from './services/login.service';
import { HomeService } from './services/home.service';
import { GeneralSettingService } from './services/generalsetting.service';
import { NotificationService } from './services/notification.service';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from "ngx-spinner";
import { AppGlobal } from './app.global';

import { LoginComponent } from '../app/component/Login/login.component';
import { HomeComponent } from '../app/component/Home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    NgxPaginationModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    FormsModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule,
  ],
  providers: [AppGlobal, LoginService, HomeService, GeneralSettingService, NotificationService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
