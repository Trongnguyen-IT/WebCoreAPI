import axios from "axios";
import { localStoredKey } from "~/enums/localStoredKey";
import { getToken } from "./localStoredManager";
import { refreshToken } from "./refreshToken";

const httpRequest = axios.create({
  baseURL: process.env.REACT_APP_API_ENDPOINT,
  timeout: 300000,
});

export const authHttpRequest = axios.create({
  baseURL: process.env.REACT_APP_API_ENDPOINT,
  timeout: 300000,
});

// Add a request interceptor
authHttpRequest.interceptors.request.use(
  function (config) {
    const { accessToken = null } = JSON.parse(getToken(localStoredKey.token));

    config.headers = {
      ...config.headers,
      Authorization: `Bearer ${accessToken}`,
      //'Accept': 'application/json',
      //'Content-Type': 'application/x-www-form-urlencoded'
    };

    //console.log("config", config);

    // Do something before request is sent
    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

// Add a response interceptor
authHttpRequest.interceptors.response.use(
  function (response) {
    //console.log('response', response);
    // Any status code that lie within the range of 2xx cause this function to trigger
    // Do something with response data
    return response;
  },
  async (error) => {
    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error
    //console.log("response", error);

    const originalRequest = error.config;
    if (error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      const result = await refreshToken();
      if (result && result?.accessToken) {
        originalRequest.headers = {
          ...originalRequest.headers,
          Authorization: `Bearer ${result.accessToken}`,
        };
      }

      return authHttpRequest(originalRequest);
    }

    return Promise.reject(error);
  }
);

export default httpRequest;
