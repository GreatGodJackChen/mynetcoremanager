import { MenuDataItem } from '@ant-design/pro-layout';
import { mygetAuthority } from './authority';
let hasAction: boolean = false;
let menus: MenuDataItem[] = mygetAuthority('menu');


export function checkAction(path: string, action: string): boolean {
  if (menus) {
    menus.forEach(a => {
      if (a.path == path) {
        if (a.action) {
          if (a.action.indexOf(action) > -1) {
            hasAction = true;
          }
          hasAction = false;
        }
      }
      if (a.children) {
        actionAuth(path, action, a);
      }
    });
  }
  return hasAction;;
};

export function actionAuth(path: string, action: string, menu: MenuDataItem) {
  if (menu.children) {
    menu.children.forEach(a => {
      if (a.path == path) {
        if (a.actions) {
          if (a.actions.indexOf(action) > -1) {
            hasAction = true;
          }
        } 
      }
      if (a.children) {
        actionAuth(path, action, a);
      }
    })
  }
}
