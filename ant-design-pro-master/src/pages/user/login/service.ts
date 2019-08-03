import request from 'umi-request';
import { FromDataType } from './index';
import { APIV1} from '../../../utils/config';

//export async function fakeAccountLogin(params: FromDataType) {
//  return request('/api/login/account', {
//    method: 'POST',
//    data: params,
//  });
//}

export async function fakeAccountLogin(params: FromDataType) {
  return request(`${APIV1}/Auth`, {
    method: 'POST',
    data: params,
  });
};


export async function getFakeCaptcha(mobile: string) {
  return request(`/api/login/captcha?mobile=${mobile}`);
}
