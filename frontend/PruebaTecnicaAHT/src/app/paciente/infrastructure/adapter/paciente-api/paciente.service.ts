import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import ENVIRONMENTS from '../../../../../environments/config';
import { CrearPacienteRequest, CrearPacienteResponse, ObtenerListadoGeneroResponse, ObtenerListadoPacientesRequest, ObtenerListadoPacientesResponse, ObtenerListadoTipoDocumentoResponse } from '../../../domain/models/pacientes-model';

@Injectable({
  providedIn: 'root',
})
export class PacienteService {
  private readonly http = inject(HttpClient);

 obtenerListadoPacientes(
    request: ObtenerListadoPacientesRequest
  ): Observable<ObtenerListadoPacientesResponse> {
    const params = new HttpParams()
      .set('numeroPagina', request.numeroPagina)
      .set('numeroFilas', request.numeroFilas);

    return this.http.get<ObtenerListadoPacientesResponse>(
      ENVIRONMENTS.GET_LISTADO_PACIENTES,
      { params }
    );
  }

  crearPaciente(request: CrearPacienteRequest): Observable<CrearPacienteResponse> {
    return this.http.post<CrearPacienteResponse>(
      ENVIRONMENTS.CREATE_PACIENTE,
      request
    );
  }

  obtenerListadoTipoDocumento(): Observable<ObtenerListadoTipoDocumentoResponse[]> {
    return this.http.get<ObtenerListadoTipoDocumentoResponse[]>(
      ENVIRONMENTS.GET_LISTADO_TIPO_DOCUMENTO
    );
  }

  obtenerListadoGenero(): Observable<ObtenerListadoGeneroResponse[]> {
    return this.http.get<ObtenerListadoGeneroResponse[]>(
      ENVIRONMENTS.GET_LISTADO_GENERO
    );
  }


}
