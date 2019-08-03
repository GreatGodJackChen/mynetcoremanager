import request from '@/utils/request';
import { TableListParams } from './data.d';
import { APIV1 } from '../../../utils/config';
import { getToken } from '../../../clientapp';

export async function queryRule(params: TableListParams){
  //return request(`${APIV1}/Permission?paramsInfo=${params}`)
  const token = getToken();
  return request(`${APIV1}/Permission`, {
    params
  });
}

export async function removeRule(params: TableListParams) {
  return request(`${APIV1}/Permission/deletePermission`, {
    method: 'Delete',
    data: {
      ...params,
      method: 'delete',
    },
  });
}

export async function addRule(params: TableListParams) {
  return request(`${APIV1}/Permission`, {
    method: 'POST',
    data: {
      ...params,
    },
  });
}

export async function updateRule(params: TableListParams) {
  return request(`${APIV1}/Permission/updatePermission`, {
    method: 'POST',
    data: {
      ...params,
    },
  });
}
