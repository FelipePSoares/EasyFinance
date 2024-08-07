/// <reference types="@angular/localize" />

import { bootstrapApplication } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { routes } from './app/features/app-routing.module';
import { HttpRequestInterceptor } from './app/core/interceptor/http-request-interceptor';
import { LoadingInterceptor } from './app/core/interceptor/loading.interceptor';

import { AppComponent } from './app/features/app.component';
import { createMap } from '@automapper/core';
import { mapper } from './app/core/utils/mappings/mapper';
import { Project } from './app/core/models/project';
import { ProjectDto } from './app/features/project/models/project-dto';
import { Income } from './app/core/models/income';
import { IncomeDto } from './app/features/income/models/income-dto';

bootstrapApplication(AppComponent, {
  providers: [
    provideAnimations(),
    provideRouter(routes, withComponentInputBinding()),
    provideHttpClient(
      withInterceptors([HttpRequestInterceptor, LoadingInterceptor]))],
});

createMap(mapper, Project, ProjectDto);
createMap(mapper, Income, IncomeDto);
