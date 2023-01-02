import { authHttpRequest } from "~/utilities/httpRequest";

const getImages = (url, params) => {
  return authHttpRequest.get(url, { params: params });
};

const uploadImage = (url, data) => {
  return authHttpRequest.post(url, data);
};

export { getImages, uploadImage };
