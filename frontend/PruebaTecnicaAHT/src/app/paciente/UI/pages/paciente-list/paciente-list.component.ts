import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { PacienteUsecase } from '../../../domain/usecase/paciente.usecase';
import { PacienteListadoItemResponse } from '../../../domain/models/pacientes-model';

@Component({
  selector: 'app-paciente-list',
  standalone: true,
  imports: [CommonModule, RouterLink, DatePipe],
  templateUrl: './paciente-list.component.html',
  styleUrl: './paciente-list.component.scss',
})
export class PacienteListComponent implements OnInit {
  private readonly pacienteUsecase = inject(PacienteUsecase);

  pacientes = signal<PacienteListadoItemResponse[]>([]);
  totalPacientes = signal(0);
  cargando = signal(false);
  error = signal('');
  numeroPagina = signal(1);
  numeroFilas = signal(10);
  terminoBusqueda = signal('');

  pacientesFiltrados = computed(() => {
    const termino = this.terminoBusqueda().trim().toLowerCase();

    if (!termino) {
      return this.pacientes();
    }

    return this.pacientes().filter((paciente) => {
      return (
        paciente.nombrePaciente.toLowerCase().includes(termino) ||
        paciente.numeroDocumento.toLowerCase().includes(termino) ||
        paciente.correoElectronico.toLowerCase().includes(termino) ||
        (paciente.telefono ?? '').toLowerCase().includes(termino)
      );
    });
  });

  ngOnInit(): void {
    this.obtenerListadoPacientes();
  }

  obtenerListadoPacientes(): void {
    this.cargando.set(true);
    this.error.set('');

    this.pacienteUsecase
      .obtenerListadoPacientes({
        numeroPagina: this.numeroPagina(),
        numeroFilas: this.numeroFilas(),
      })
      .subscribe({
        next: (respuesta) => {
          this.totalPacientes.set(respuesta.totalPacientes);
          this.pacientes.set(respuesta.pacientes);
          this.cargando.set(false);
        },
        error: () => {
          this.error.set('error al obtener listado pacientes');
          this.cargando.set(false);
        },
      });
  }

  cambiarTerminoBusqueda(evento: Event): void {
    const valor = (evento.target as HTMLInputElement).value;
    this.terminoBusqueda.set(valor);
  }

  limpiarBusqueda(): void {
    this.terminoBusqueda.set('');
  }

  cambiarNumeroFilas(evento: Event): void {
    const valor = Number((evento.target as HTMLSelectElement).value);
    this.numeroFilas.set(valor);
    this.numeroPagina.set(1);
    this.obtenerListadoPacientes();
  }

  paginaAnterior(): void {
    if (this.numeroPagina() <= 1) {
      return;
    }

    this.numeroPagina.set(this.numeroPagina() - 1);
    this.obtenerListadoPacientes();
  }

  paginaSiguiente(): void {
    this.numeroPagina.set(this.numeroPagina() + 1);
    this.obtenerListadoPacientes();
  }

  obtenerValor(valor?: string | null): string {
    return valor && valor.trim() ? valor : '-';
  }
}
