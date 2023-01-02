import httpRequest, { authHttpRequest } from "~/utilities/httpRequest";

const getProducts = (url, params) => {
  return authHttpRequest.get(url, { params: params });
};

const getById = (url, id) => {
  return httpRequest.get(`${url}/${id}`);
};

const createProduct = (url, data) => {
  return authHttpRequest.post(url, data);
};

const updateProduct = (url, data) => {
  return authHttpRequest.put(url, data);
};

const deleteProduct = (url, id) => {
  return authHttpRequest.delete(`${url}/${id}`);
};

export { getProducts, getById, createProduct, updateProduct, deleteProduct };
