import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountService } from './services/account.service';
import {
  HttpClientModule,
  provideHttpClient,
  withInterceptors,
} from '@angular/common/http';
import { RegisterComponent } from './components/register/register.component';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { tokenInterceptor } from './interceptor/token-interceptor';
import { MenuComponent } from './components/menu/menu.component';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    MenuComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    RouterModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    MatIconModule,
  ],
  providers: [provideHttpClient(withInterceptors([tokenInterceptor]))],
  bootstrap: [AppComponent],
})
export class AppModule {}
