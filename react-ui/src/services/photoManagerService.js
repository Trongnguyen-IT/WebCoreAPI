import httpRequest from '~/utilities/httpRequest';

const getImages = (url, params) => {
    return httpRequest.get(url, { params: params })
}

const uploadImage = (url, data) => {
    return httpRequest.post(url, data)
}

export { getImages, uploadImage }