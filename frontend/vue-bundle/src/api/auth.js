import { SetAuthorizationToken } from './index';
import axios from '.';

export const login = async (email, password) => {
    try {
        const { data } = await axios.post('/auth/login', JSON.stringify({login: email, password: password}));
        if (data.isSuccess) {
            SetAuthorizationToken(data.result.token);
            localStorage.setItem('authToken', data.result.token);
        }
        return data.isSuccess;
    } catch (error) {
        return false;
    }
}


const auth = {
    login
};

export default auth;