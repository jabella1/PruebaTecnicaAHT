import { CommonModule } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ObtenerListadoGeneroResponse, ObtenerListadoTipoDocumentoResponse } from '../../../domain/models/pacientes-model';
import { PacienteUsecase } from '../../../domain/usecase/paciente.usecase';

@Component({
  selector: 'app-paciente-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './paciente-create.component.html',
  styleUrl: './paciente-create.component.scss',
})
export class PacienteCreateComponent implements OnInit {
  private readonly formBuilder = inject(FormBuilder);
  private readonly pacienteUsecase = inject(PacienteUsecase);
  private readonly router = inject(Router);

  tiposDocumento = signal<ObtenerListadoTipoDocumentoResponse[]>([]);
  generos = signal<ObtenerListadoGeneroResponse[]>([]);
  cargandoCatalogos = signal(false);
  guardando = signal(false);
  error = signal('');
  mensajeExito = signal('');

  formulario = this.formBuilder.group({
    tipoDocumentoId: [0, [Validators.required, Validators.min(1)]],
    numeroDocumento: ['', [Validators.required, Validators.maxLength(50)]],
    nombrePaciente: ['', [Validators.required, Validators.maxLength(200)]],
    fechaNacimiento: ['', [Validators.required]],
    correoElectronico: ['', [Validators.required, Validators.email, Validators.maxLength(200)]],
    generoId: [0, [Validators.required, Validators.min(1)]],
    direccion: ['', [Validators.maxLength(250)]],
    telefono: ['', [Validators.maxLength(30)]],
  });

  ngOnInit(): void {
    this.obtenerCatalogos();
  }

  obtenerCatalogos(): void {
    this.cargandoCatalogos.set(true);
    this.error.set('');

    this.pacienteUsecase.obtenerListadoTipoDocumento().subscribe({
      next: (respuesta) => {
        this.tiposDocumento.set(respuesta);
      },
      error: () => {
        this.error.set('error al obtener tipo documento');
        this.cargandoCatalogos.set(false);
      },
    });

    this.pacienteUsecase.obtenerListadoGenero().subscribe({
      next: (respuesta) => {
        this.generos.set(respuesta);
        this.cargandoCatalogos.set(false);
      },
      error: () => {
        this.error.set('error al obtener genero');
        this.cargandoCatalogos.set(false);
      },
    });
  }

  crearPaciente(): void {
    this.formulario.markAllAsTouched();
    this.error.set('');
    this.mensajeExito.set('');

    if (this.formulario.invalid) {
      return;
    }

    this.guardando.set(true);

    const formularioValor = this.formulario.getRawValue();

    this.pacienteUsecase
      .crearPaciente({
        tipoDocumentoId: Number(formularioValor.tipoDocumentoId),
        numeroDocumento: formularioValor.numeroDocumento?.trim() ?? '',
        nombrePaciente: formularioValor.nombrePaciente?.trim() ?? '',
        fechaNacimiento: formularioValor.fechaNacimiento ?? '',
        correoElectronico: formularioValor.correoElectronico?.trim() ?? '',
        generoId: Number(formularioValor.generoId),
        direccion: formularioValor.direccion?.trim() || null,
        telefono: formularioValor.telefono?.trim() || null,
      })
      .subscribe({
        next: () => {
          this.guardando.set(false);
          this.mensajeExito.set('paciente creado correctamente');
          this.formulario.reset({
            tipoDocumentoId: 0,
            numeroDocumento: '',
            nombrePaciente: '',
            fechaNacimiento: '',
            correoElectronico: '',
            generoId: 0,
            direccion: '',
            telefono: '',
          });

          setTimeout(() => {
            this.router.navigate(['/paciente']);
          }, 800);
        },
        error: () => {
          this.guardando.set(false);
          this.error.set('error al crear paciente');
        },
      });
  }

  campoInvalido(nombreCampo: string): boolean {
    const campo = this.formulario.get(nombreCampo);
    return !!campo && campo.invalid && campo.touched;
  }
}
