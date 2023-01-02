import axios from "axios";
import { localStoredKey } from "~/enums/localStoredKey";
import { getToken } from "./localStoredManager";
import { refreshAccessToken } from "~/services/authService";

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
    const token = getToken(localStoredKey.accessToken);
    config.headers = {
      ...config.headers,
      Authorization: `Bearer ${token}`,
      //'Accept': 'application/json',
      //'Content-Type': 'application/x-www-form-urlencoded'
    };

    //console.log('config', config);

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
    console.log("response", error);

    const originalRequest = error.config;
    if (error.response.status === 401 && !originalRequest._retry) {
      const token = getToken(localStoredKey.accessToken);
      const refreshToken = getToken(localStoredKey.refreshToken);
      originalRequest._retry = true;
      const access_token = await refreshAccessToken("User/RefreshToken", {
        accessToken: token,
        refreshToken,
      });
      authHttpRequest.defaults.headers.common["Authorization"] =
        "Bearer " + access_token;
      return authHttpRequest(originalRequest);
    }

    return Promise.reject(error);
  }
);

export default httpRequest;
