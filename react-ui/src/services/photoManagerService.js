import httpRequest from '~/utilities/httpRequest';

const getImages = (url, payload) => {
    return httpRequest.get(url)
}

const uploadImage = (url, data) => {
    return httpRequest.post(url, data)
}

export { uploadImage }