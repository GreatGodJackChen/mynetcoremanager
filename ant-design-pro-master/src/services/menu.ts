import request from '@/utils/request';
import { APIV1 } from '../utils/config';

export  function getMenuData(){
  return request(`${APIV1}/Menu`);
}
