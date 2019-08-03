import Oidc from "oidc-client";

const config = {
  authority: "http://localhost:5000",
  client_id: "js",
  redirect_uri: "http://localhost:8000/Callback",
  response_type: "code",
  scope: "openid profile jsapi",
  post_logout_redirect_uri: "http://localhost:8000",
};
let token = null;
export let mgr = new
  Oidc.UserManager(config);


mgr.getUser().then(function (user) {
  if (user) {
    alert(user);
  }
  else {
    alert(user);
  }
});


export const login= function() {
  mgr.signinRedirect();
};
export function logout() {
  mgr.signoutRedirect();
};
export function getCookie(name:string) {
  var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
  if (arr = document.cookie.match(reg))
    return unescape(arr[2]);
  else
    return null;
}
function setToken() {
   mgr.getUser().then(function (user: any) {
    if (user) {
      token=user.access_token;
      alert(user.access_token);
      return user.access_token;
    }
  });
};
export function getToken(){
  setToken()
alert(token);
  return token;
}

