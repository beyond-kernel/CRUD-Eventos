import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { DateTimeFormatPipePipe } from './_helper/date-time-format-pipe.pipe';
import { EventoService } from './_services/evento.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';



@NgModule({
  declarations: [AppComponent, EventosComponent, NavComponent, DateTimeFormatPipePipe, DateTimeFormatPipePipe],
  imports: [BrowserModule,
    BsDatepickerModule.forRoot(),
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
     HttpClientModule, FormsModule,
     FontAwesomeModule, ModalModule.forRoot(),
      ReactiveFormsModule ],
  providers: [EventoService],
  bootstrap: [AppComponent],
})
export class AppModule {}
