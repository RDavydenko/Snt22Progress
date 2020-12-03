import axios from '@/api';
import { SET_STATE, SET_LOADING } from '@/utils/mutations';

export default {
    namespaced: true,
    state: {
        user: {},
        loading: false
    },
    getters: {
        user: state => state.user,
        loading: state => state.loading
    },
    mutations: {
        SET_STATE,
        SET_LOADING
    },
    actions: {
        async fetchUser({ state, commit }) {
            try {
                commit('SET_LOADING', { value: true });
                let { data } = await axios.get('/users/about');
                if (data.isSuccess) {
                    commit('SET_STATE', { paramName: 'user', value: data.result });
                }
                else {
                    commit('SET_STATE', { paramName: 'user', value: {} });
                }
            } catch (error) {

            } finally {
                commit('SET_LOADING', { value: false });
            }
        },
        async updateUser({ state, commit }, value) {
            try {
                commit('SET_LOADING', { value: true });
                let { data } = await axios.post('/users/edit', JSON.stringify(value));
                if (data.isSuccess) {
                    commit('SET_STATE', { paramName: 'user', value: data.result });
                }
            } catch (error) {

            } finally {
                commit('SET_LOADING', { value: false });
            }
        }
    }
};