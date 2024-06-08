import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItemType } from 'ng-devui/menu';
import { debounceTime, fromEvent } from 'rxjs';
import { navigation } from 'src/app/app-navigation';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainLayoutComponent {

  collapsed: boolean = false;
  menus: MenuItemType[] = [];

  openKeys: string[] = [''];
  activeKey = '';

  resize$: any;

  loginUser: { 
    name: string, 
    email: string 
  } = { 
    name: 'BennyYang', 
    email: 'BennyYang@cobaltknitwear.com' 
  }

  constructor(private router: Router) {
    this.menus = navigation;
  }

  ViewChanges() {
    this.collapsed = window.innerWidth <= 800;
    console.log(this.collapsed);
  }

  ngOnInit() {
    this.resize$ = fromEvent(window, 'resize')
    .pipe(debounceTime(200))
    .subscribe((e) => {
      this.ViewChanges();
    })
  }

  openChange(open: boolean, key: string) {
    if (open) {
      this.openKeys.push(key);
    } else {
      this.openKeys = this.openKeys.filter(item => item !== key);
    }
  }

  itemClick(key: string) {
    this.activeKey = key;
    this.router.navigate([this.activeKey]);
  }

  trackByMenu(_: number, item: MenuItemType) {
    return item.key;
  }


}
