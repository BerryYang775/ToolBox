import { Component, OnInit, ChangeDetectorRef, ViewChild, ErrorHandler } from '@angular/core';
import { DataTableComponent, DialogService, LoadingType, TableCheckOptions, TableWidthConfig, ToastService } from 'ng-devui';
import { Observable } from 'rxjs';
import { Todo, TodoService } from 'src/api_client';
import { TodoCreateComponent } from 'src/app/shared/compoments';

@Component({
  selector: 'app-todo-main',
  templateUrl: './todo-main.component.html',
  styleUrls: ['./todo-main.component.scss']
})
export class TodoMainComponent implements OnInit {

  showLoading: boolean = false;
  // loading: LoadingType;

  createTodo: any = {
    caption: '',
    category: {}
  };

  modalConfig = {
    content: TodoCreateComponent,
    backdropCloseable: true,
    onClose: () => { this.onLoad() },
  }

  dsTodo: any[] = [];

  constructor(private dialogService: DialogService, 
    private todoService: TodoService, 
    private toastService: ToastService, 
    private changeDetectorRef: ChangeDetectorRef,
    private errsHandler: ErrorHandler) {

  }

  ngOnInit(): void {
    this.showLoading = true;
    this.onLoad();
  }

  onLoad() {
    var getTodoList$ = this.todoService.getTodoList();
    getTodoList$.subscribe({
      next: (r) => {
        this.dsTodo = r;
      },
      error: (e) => {
        this.errsHandler.handleError(e);
      },
      complete: () => {
        this.showLoading = false;
        this.changeDetectorRef.markForCheck();
      }
    })
  }

  btnCreate_click() {
    const result = this.dialogService.open({
      ...this.modalConfig,
      id: 'create-todo',
      width: '500px',
      title: 'New Todo',
      placement: "center",
      data: this.createTodo,
      buttons: [
        {
          cssClass: 'primary',
          text: 'Create',
          disabled: false,
          handler: ($event: Event) => {
            this.todoService.addTodo(this.createTodo).subscribe({
              next: () => {
                this.toastService.open({
                  value: [{ severity: 'success', summary: 'Success', content: 'Create Success' }],
                });
              },
              error: (e) => {
                this.errsHandler.handleError(e);
              }
            })
            result.modalInstance.hide();
          }
        },
        {
          id: 'btn-cancel',
          cssClass: 'common',
          text: 'cancel',
          handler: ($event: Event) => {
            result.modalInstance.hide();
          }
        }
      ]
    });
  }

  btnDone_click(event: any){
    this.todoService.finishTodo(event.todoID).subscribe({
      next: (r) => {
        this.toastService.open({
          value: [{ severity: 'success', summary: 'Finish', content: "" }],
        });
        this.onLoad();
      },
      error: (e) => {
        this.errsHandler.handleError(e);
      },
    })
  }

  btnEdit_click(data: any){
    const result = this.dialogService.open({
      ...this.modalConfig,
      id: 'save-todo',
      width: '500px',
      title: 'Save Todo',
      placement: "center",
      data: data,
      buttons: [
        {
          cssClass: 'primary',
          text: 'Save',
          disabled: false,
          handler: ($event: Event) => {
            this.todoService.updateTodo(data.todoID, data).subscribe({
              next: () => {
                this.toastService.open({
                  value: [{ severity: 'success', summary: 'Save Success', content: '' }],
                });
              },
              error: (e) => {
                this.errsHandler.handleError(e);
              }
            })
            result.modalInstance.hide();
          }
        },
        {
          id: 'btn-cancel',
          cssClass: 'common',
          text: 'cancel',
          handler: ($event: Event) => {
            result.modalInstance.hide();
          }
        }
      ]
    });
  }
}
