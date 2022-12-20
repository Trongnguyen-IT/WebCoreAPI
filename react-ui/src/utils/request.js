import axios from "axios";

const request = axios.create({
    baseURL: 'https://localhost:7052/api/',
    timeout: 1000,
  });

  export default request