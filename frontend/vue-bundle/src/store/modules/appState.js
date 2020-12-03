import axios from '@/api';
import { SET_STATE } from '@/utils/mutations';
import { SetAuthorizationToken } from "@/api/index"

export default {
    namespaced: true,
    state: {
        isAuth: false,
        user: {},
        access: [],
        loading: false  
    },
    getters: {
        isAuth: state => state.isAuth,
        user: state => state.user,
        access: state => state.access,
        loading: state => state.loading
    },
    mutations: {
        SET_STATE
    },
    actions: {
        async fetchAppState({ state, commit }) {
            try {
                SetAuthorizationToken(localStorage.getItem('authToken'));

                commit('SET_STATE', { paramName: 'loading', value: true });
                let { data } = await axios.get('/users/is-auth');
                commit('SET_STATE', { paramName: 'isAuth', value: Boolean(data.isSuccess) });                

                if (state.isAuth) {
                    let { data } = await axios.get('/users/about');
                    if (data.isSuccess) {
                        commit('SET_STATE', { paramName: 'user', value: data.result });
                    }
                }
                else {
                    commit('SET_STATE', { paramName: 'user', value: {} });
                }

                if (state.isAuth) {
                    let { data } = await axios.get('/users/access');
                    if (data.isSuccess) {
                        commit('SET_STATE', { paramName: 'access', value: data.result });
                    }
                }
                else {
                    commit('SET_STATE', { paramName: 'access', value: [] });
                }

                commit('SET_STATE', { paramName: 'loading', value: false });
            } catch (error) {

            } finally {
                commit('SET_STATE', { paramName: 'loading', value: false });
            }
        }
    }
};