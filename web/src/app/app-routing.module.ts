import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LibraryComponent } from './components/library/library.component';
import { LoginComponent } from './components/login/login.component'
import { RegisterComponent } from './components/register/register.component'
import { SplashComponent } from './components/splash/splash.component';

const routes: Routes = [
  {
    path: '',
    component: SplashComponent,
    data: { title: 'Splash' },
  },
  {
    path: 'login',
    component: LoginComponent,
    data: { title: 'Login' },
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: { title: 'Register' },
  },
  {
    path: 'library',
    component: LibraryComponent,
    data: { title: 'Library' },
  },
  {
    path: '**',
    component: SplashComponent,
    data: { title: 'Splash' },
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
