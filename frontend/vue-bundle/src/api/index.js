import axiosInstance from 'axios';

// Конфигурация axios
const axios = axiosInstance.create({
    baseURL: 'https://localhost:44372/api',
    responseType: 'json',
    headers: {
        'Content-Type': 'application/json; charset=utf-8',
        'Authorization': 'Bearer ' + localStorage.getItem('authToken')
    }
});

window.axios = axios;

export const SetAuthorizationToken = async (token) => {
    if (token != null) {
        axios.defaults.headers.common = {'Authorization': `Bearer ${token}`}
    }
}

export default axios;
