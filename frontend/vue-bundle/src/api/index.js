import axiosInstance from 'axios';

// Конфигурация axios
const axios = axiosInstance.create({
    baseURL: 'http://185.22.173.199:1000/api',
    responseType: 'json',
    headers: {
        'Content-Type': 'application/json; charset=utf-8'
    }
});

window.axios = axios;

export const SetAuthorizationToken = (token) => {   
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;    
}

export default axios;
