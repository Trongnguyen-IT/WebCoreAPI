import httpRequest from "~/utilities/httpRequest";

const authentication = (url, data) => {
  return httpRequest.post(url, data);
};

const refreshAccessToken = (url, data) => {
  return httpRequest.post(url, data);
};

export { authentication, refreshAccessToken };
