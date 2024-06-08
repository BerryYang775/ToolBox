export * from './master.service';
import { MasterService } from './master.service';
export * from './todo.service';
import { TodoService } from './todo.service';
export const APIS = [MasterService, TodoService];
