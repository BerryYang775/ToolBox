import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {
  DashboardComponent,
  TodoMainComponent
} from './page/index'

import {
  SimpleMainLayoutComponent
} from './layout/index'

import { 
  ButtonModule, 
  DataTableModule, 
  IconModule,
  FormModule,
  SelectModule,
  ToastModule,
  LoadingModule
} from 'ng-devui';

import {
  TodoCreateComponent
} from './shared/compoments/index'
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

const devui = [
  IconModule,
  ButtonModule,
  DataTableModule,
  FormModule,
  SelectModule,
  ToastModule,
  LoadingModule
]

const page = [
  DashboardComponent,
  TodoMainComponent,
  SimpleMainLayoutComponent
]

const component = [
  TodoCreateComponent
]

const routes: Routes = [
  {
    path: 'dashboard',
    title: 'Dashboard',
    component: DashboardComponent
  },
  {
    path: 'todo',
    title: 'Todo',
    children: [
      {
        path: 'list',
        title: 'TodoMain',
        component: TodoMainComponent
      }
    ]
  }
];


@NgModule({
  declarations: [
    ...page,
    ...component
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes),
    ...devui,
    FormsModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
