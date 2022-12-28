import httpRequest from "~/utilities/httpRequest";

const getProducts = (url, params) => {
  return httpRequest.get(url, { params: params });
};

const getById = (url, id) => {
  return httpRequest.get(`${url}/${id}`);
};

const createProduct = (url, data) => {
  return httpRequest.post(url, data);
};

const updateProduct = (url, data) => {
  return httpRequest.put(url, data);
};

const deleteProduct = (url, id) => {
  return httpRequest.delete(`${url}/${id}`);
};

export { getProducts, getById, createProduct, updateProduct, deleteProduct };
