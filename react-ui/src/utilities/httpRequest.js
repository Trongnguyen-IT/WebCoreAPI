import axios from "axios";

const httpRequest = axios.create({
  baseURL: process.env.REACT_APP_API_ENDPOINT,
  timeout: 30000,
});

httpRequest.defaults.headers.common['Authorization'] = 'AUTH_TOKEN';
// Add a request interceptor
httpRequest.interceptors.request.use(function (config) {
  //console.log('config', config);
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
}, function (error) {
  // Any status codes that falls outside the range of 2xx cause this function to trigger
  // Do something with response error
  console.log(error);
  return Promise.reject(error);
});

export default httpRequest