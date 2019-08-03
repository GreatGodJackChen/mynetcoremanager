import React from 'react';
import Oidc from "oidc-client";
import { Redirect } from 'react-router-dom';

export default (): React.ReactNode => (
  new Oidc.UserManager({ response_mode: "query" }).signinRedirectCallback().then(function () {
    <Redirect push to="/" />
  }).catch(function (e) {
    console.error(e);
  })
)
