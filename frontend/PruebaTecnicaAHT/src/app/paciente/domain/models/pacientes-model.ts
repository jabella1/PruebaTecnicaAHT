export interface ObtenerListadoPacientesRequest {
  numeroPagina: number;
  numeroFilas: number;
}

export interface PacienteListadoItemResponse {
  pacienteId: number;
  tipoDocumentoId: number;
  numeroDocumento: string;
  nombrePaciente: string;
  fechaNacimiento: string;
  correoElectronico: string;
  generoId: number;
  direccion?: string | null;
  telefono?: string | null;
}

export interface ObtenerListadoPacientesResponse {
  totalPacientes: number;
  pacientes: PacienteListadoItemResponse[];
}

export interface CrearPacienteRequest {
  tipoDocumentoId: number;
  numeroDocumento: string;
  nombrePaciente: string;
  fechaNacimiento: string;
  correoElectronico: string;
  generoId: number;
  direccion?: string | null;
  telefono?: string | null;
}

export interface CrearPacienteResponse {
  pacienteId: number;
}

export interface ObtenerListadoTipoDocumentoResponse {
  id: number;
  tipo: string;
}

export interface ObtenerListadoGeneroResponse {
  id: number;
  nombre: string;
}
