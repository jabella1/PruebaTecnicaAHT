import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PacienteService } from '../../infrastructure/adapter/paciente-api/paciente.service';
import { CrearPacienteResponse, CrearPacienteRequest, ObtenerListadoPacientesRequest, ObtenerListadoPacientesResponse, ObtenerListadoTipoDocumentoResponse, ObtenerListadoGeneroResponse } from '../models/pacientes-model';

@Injectable({
  providedIn: 'root',
})
export class PacienteUsecase {
  private readonly servicio = inject(PacienteService);

  obtenerListadoPacientes(
    request: ObtenerListadoPacientesRequest
  ): Observable<ObtenerListadoPacientesResponse> {
    return this.servicio.obtenerListadoPacientes(request);
  }

  crearPaciente(request: CrearPacienteRequest): Observable<CrearPacienteResponse> {
    return this.servicio.crearPaciente(request);
  }

  obtenerListadoTipoDocumento(): Observable<ObtenerListadoTipoDocumentoResponse[]> {
    return this.servicio.obtenerListadoTipoDocumento();
  }

  obtenerListadoGenero(): Observable<ObtenerListadoGeneroResponse[]> {
    return this.servicio.obtenerListadoGenero();
  }
}
