import httpRequest from "~/utilities/httpRequest";

const getProducts = (url, params) => {
    return httpRequest.get(url, { params: params })
}

const createProduct = (url, data) => {
    return httpRequest.post(url, data)
}

export { getProducts, createProduct }