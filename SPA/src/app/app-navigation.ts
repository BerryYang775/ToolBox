import { MenuItemType } from 'ng-devui/menu';

export const navigation: MenuItemType[] = [
    {
        key: 'dashboard',
        name: 'Dashboard',
        icon: 'icon-line-chart',
    },
    {
        key: 'todo',
        name: 'Todo',
        icon: 'icon-add-tasklist',
        children: [
            {
                key: 'todo/list',
                name: 'List',
                icon: 'icon-op-list'
            }
        ]
    },
];
