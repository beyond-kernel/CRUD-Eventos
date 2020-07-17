import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Evento } from '../_models/evento';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baseURL = 'https://localhost:5001/site/Evento';

  constructor(private http: HttpClient) { }
  getAllEvento(): Observable<Evento[]>{
    return this.http.get<Evento[]>(this.baseURL);
  }
  getEventoByTema(tema: string): Observable<Evento[]>{
    return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${tema}`);
  }
  getEventoById(id: number): Observable<Evento[]>{
    return this.http.get<Evento[]>(`${this.baseURL}/getById/${id}`);
  }

  postEvento(evento: Evento){
    return this.http.post(`${this.baseURL}`, evento);
  }

  putEvento(evento: Evento){
    return this.http.put(`${this.baseURL}`, evento);
  }

  deleteEvento(evento: Evento){
    return this.http.delete(`${this.baseURL}/Delete/${evento.eventoId}`);
  }

  postUpload(file: File, nameFile: string){
    const fileToUpload = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, nameFile);
    return this.http.post(`${this.baseURL}/upload`, formData);
  }
}
