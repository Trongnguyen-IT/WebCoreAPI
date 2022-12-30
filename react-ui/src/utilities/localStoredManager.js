const getToken = (key) => {
    return localStorage.getItem(key)
}

const setToken = (key, value) => {
    return localStorage.setItem(key, value)
}


const removeToken = (key) => {
    return localStorage.removeItem(key)
}

export { getToken, setToken, removeToken }