import axios from "axios";
import { localStoredKey } from "~/enums/localStoredKey";
import { getToken } from "./localStoredManager";

const httpRequest = axios.create({
  baseURL: process.env.REACT_APP_API_ENDPOINT,
  timeout: 300000,
});


// Add a request interceptor
httpRequest.interceptors.request.use(function (config) {
  const token = getToken(localStoredKey.accessToken)
  config.headers = {
    ...config.headers,
    'Authorization': `Bearer ${token}`,
    //'Accept': 'application/json',
    //'Content-Type': 'application/x-www-form-urlencoded'
  }

  console.log('config', config);

  // Do something before request is sent
  return config;
}, function (error) {
  // Do something with request error
  return Promise.reject(error);
});

// Add a response interceptor
httpRequest.interceptors.response.use(function (response) {
  //console.log('response', response);
  // Any status code that lie within the range of 2xx cause this function to trigger
  // Do something with response data
  return response;
}, async (error) => {
  // Any status codes that falls outside the range of 2xx cause this function to trigger
  // Do something with response error
  console.log('response', error);

  // const originalRequest = error.config;
  // if (error.response.status === 403 && !originalRequest._retry) {
  //   originalRequest._retry = true;
  //   const access_token = await refreshAccessToken();            
  //   axios.defaults.headers.common['Authorization'] = 'Bearer ' + access_token;
  //   return httpRequest(originalRequest);
  // }

  return Promise.reject(error);
});

export default httpRequest