export default {
    namespaced: true,
    state: {
        isAuth: false
    },
    getters: {
        isAuth: (state) => Boolean(localStorage.getItem('authToken')) || state.isAuth
    },
    mutations: {
        setAuth(state, value) {
            state.isAuth = Boolean(value);
        }
    }
};