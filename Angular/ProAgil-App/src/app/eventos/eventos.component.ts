import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/evento';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, ptBrLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';


defineLocale('pt-br', ptBrLocale);


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css'],
})
export class EventosComponent implements OnInit {
  eventos: Evento[];
  evento: Evento;
  imgAltura = 50;
  imgMargem = 2;
  showImage = false;
  filterLista: string;
  registerForm: FormGroup;
  modoSalvar = 'post';
  bodyDeletarEvento = '';

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              private fb: FormBuilder,
              private localeService: BsLocaleService,
              private toastr: ToastrService){this.localeService.use('pt-br');
            }

  get filtroLista(): string {
    return this.filterLista;
  }
  set filtroLista(value: string) {
    this.filterLista = value;
    this.eventosFiltrados = this.filterLista
      ? this.filtrarEvento(this.filtroLista)
      : this.eventos;
  }

  eventosFiltrados: Evento[];

  // eventos: any = [
  //   {
  //     Eventoid: 1,
  //     Tema: 'Angular',
  //     Local: 'SP',
  //   },
  //   {
  //     Eventoid: 2,
  //     Tema: '.Net Core',
  //     Local: 'RJ',
  //   },
  //   {
  //     Eventoid: 3,
  //     Tema: '.Net Core e Angular',
  //     Local: 'MS',
  //   },
  // ];

  alterImage() {
    this.showImage = !this.showImage;
  }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  openModal(template: any){
    this.registerForm.reset();
    template.show();
  }

  editarEvento(evento: Evento, template: any){
    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.tema}`;
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento).subscribe(
      () => {
          template.hide();
          this.getEventos();
          this.toastr.success('Evento deletado com sucesso', this.evento.tema);
          location.reload();
        }, error => {
          this.toastr.error(`Erro ao deletar evento: ${error}`, this.evento.tema);
          console.log(error);
        }
    );
  }

  novoEvento(template: any){
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  salvarAlteracao(template: any){
    if (this.registerForm.valid){
      if (this.modoSalvar === 'post'){
      this.evento = Object.assign({}, this.registerForm.value);
      this.eventoService.postEvento(this.evento).subscribe(
        (newEvent: Evento) => {
          console.log(newEvent);
          template.hide();
          this.getEventos();
          this.toastr.success('Evento inserido com sucesso', this.evento.tema);
        }, error => {
          console.log(error);
          this.toastr.error(`Erro ao salvar evento: ${error}`, this.evento.tema);
        }
      );
      }
      else{
        this.evento = Object.assign({eventoId: this.evento.eventoId }, this.registerForm.value);
        this.eventoService.putEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
            this.toastr.success('Evento atualizado com sucesso', this.evento.tema);
          }, error => {
            console.log(error);
            this.toastr.error(`Erro ao atualizar evento: ${error}`, this.evento.tema);
          }
        );
      }
    }
  }

validation(){
  this.registerForm = this.fb.group({
    tema: ['', Validators.required],
    local: ['', Validators.required],
    dataEvento: ['', Validators.required],
    qtdPessoas: ['', Validators.required],
    telefone: ['', Validators.required],
    email: ['', Validators.required],
    imagemUrl: ['', Validators.required]
  });
}

  filtrarEvento(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }
  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      (eventos: Evento[]) => {
        this.eventos = eventos;
      },
      (error) => {
        console.log(error);
        this.toastr.error(`Erro ao carregar evento`, 'Evento');
      }
    );
  }
}
