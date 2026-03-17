import { ENVIRONMENT } from "./environment";

const base_url_api_test = ENVIRONMENT.BASE_URL_API_TEST

const ENVIRONMENTS = {
    GET_LISTADO_PACIENTES: `${base_url_api_test}/api/paciente`,
    CREATE_PACIENTE: `${base_url_api_test}/api/paciente`,
    GET_LISTADO_TIPO_DOCUMENTO: `${base_url_api_test}/api/tipodocumento`,
    GET_LISTADO_GENERO: `${base_url_api_test}/api/genero`
}

export default ENVIRONMENTS;
