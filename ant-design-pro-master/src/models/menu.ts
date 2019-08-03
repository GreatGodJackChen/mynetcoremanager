import { getMenuData } from '@/services/menu'; // 通过后台返回特定的数组json或者mock模拟数据
import { Reducer } from 'redux';
import { Effect } from './connect';
import memoizeOne from 'memoize-one';
import isEqual from 'lodash/isEqual';
import { mysetAuthority } from '../utils/authority';
/**
 * 获取面包屑映射
 * @param {Object} menuData 菜单配置
 */
const getBreadcrumbNameMap = (menuData: object) => {
  const routerMap = {};

  const flattenMenuData = data => {
    data.forEach(menuItem => {
      if (menuItem.children) {
        flattenMenuData(menuItem.children);
      }
      // Reduce memory usage
      routerMap[menuItem.path] = menuItem;
    });
  };
  flattenMenuData(menuData);
  return routerMap;
};

const memoizeOneGetBreadcrumbNameMap = memoizeOne(getBreadcrumbNameMap, isEqual);
export interface MenuDataItem {
  name?: string;
  path: string;
  [key: string]: any;
}

export interface MenuModelState {
  menuData: any[];
}

export interface MenuModelType {
  namespace: string;
  state: MenuModelState;
  effects: {
    getMenuData: Effect;
  };
  reducers: {
    save: Reducer<MenuModelState>;
  };
}

const MenuModel: MenuModelType = {
  namespace: 'menu',
  state: {
    menuData: [],
  },
  effects: {
    *getMenuData(_, { call, put }) {
      const response = yield call(getMenuData);
      mysetAuthority('menu', response);
      yield put({
        type: 'save',
        payload: response
      });
    },
  },

  reducers: {
    save(state, action) {
      return {
        ...state,
        menuData: action.payload || [],
      };
    },
  },
};

export default MenuModel;
