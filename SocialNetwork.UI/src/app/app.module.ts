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
import { InputTextComponent } from './components/form/input-text/input-text.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { InputButtonComponent } from './components/form/input-button/input-button.component';
import { MatMenuModule } from '@angular/material/menu';
import { ChangePasswordComponent } from './components/change-password/change-password.component';

const pages = [
  LoginComponent,
  RegisterComponent,
  HomeComponent,
  ResetPasswordComponent,
  ChangePasswordComponent,
];
const formComponents = [InputTextComponent, InputButtonComponent];
const components = [MenuComponent];

@NgModule({
  declarations: [AppComponent, ...pages, ...formComponents, ...components],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    RouterModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    MatIconModule,
    MatMenuModule,
  ],
  providers: [provideHttpClient(withInterceptors([tokenInterceptor]))],
  bootstrap: [AppComponent],
})
export class AppModule {}
