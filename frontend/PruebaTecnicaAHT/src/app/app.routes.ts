import { Routes } from '@angular/router';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { HomeComponent } from './shared/components/home/home.component';

export const routes: Routes = [
{
  path: '',
  component: MainLayoutComponent,
  children: [
    {
      path: '',
      component: HomeComponent
    },
    {
      path: 'paciente',
      loadChildren: () =>
        import('./paciente/paciente.routes').then(m => m.Paciente_ROUTES)
    }
  ]
}
];
