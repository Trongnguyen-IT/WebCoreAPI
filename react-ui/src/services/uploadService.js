import httpRequest from '~/utilities/httpRequest';

const uploadImage = (url, data) => {
    return httpRequest.post(url, data)
}

export { uploadImage }