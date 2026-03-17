import { Routes } from '@angular/router';

export const Paciente_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./UI/pages/paciente-list/paciente-list.component').then(
        (m) => m.PacienteListComponent
      ),
  },
  {
    path: 'new',
    loadComponent: () =>
      import('./UI/pages/paciente-create/paciente-create.component').then(
        (m) => m.PacienteCreateComponent
      ),
  },
  {
    path: ':id/edit',
    loadComponent: () =>
      import('./UI/pages/paciente-edit/paciente-edit.component').then(
        (m) => m.PacienteEditComponent
      ),
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./UI/pages/paciente-view/paciente-view.component').then(
        (m) => m.PacienteViewComponent
      ),
  },
];
