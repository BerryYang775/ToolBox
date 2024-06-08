import { Component, ErrorHandler, Input, ViewChild } from '@angular/core';
import { FormLayout } from 'ng-devui';
import { MasterService, TodoCategory } from 'src/api_client';

@Component({
  selector: 'app-todo-create',
  templateUrl: './todo-create.component.html',
  styleUrls: ['./todo-create.component.scss']
})
export class TodoCreateComponent {
  layoutDirection: FormLayout = FormLayout.Vertical;

  todoCategories: any[] = [];

  @Input() data: any;

  @Input() handler: any;

  close($event: any) {
    this.handler($event);
  }

  constructor(private master: MasterService, private errhandleer: ErrorHandler) {
    this.master.getTodoCategories().subscribe({
      next: (r) => {
        this.todoCategories = r;
      },
      error: (e) => {
        this.errhandleer.handleError(e);
      }
    })
  }
}
