import axios from '@/api';
import { SET_STATE, SET_LOADING } from '@/utils/mutations'

export default {
    namespaced: true,
    state: {
        advertisements: [],
        loading: false
    },
    getters: {
        advertisements: state => state.advertisements,
        loading: state => state.loading
    },
    mutations: {
        SET_STATE,
        SET_LOADING
    },
    actions: {
        async fetchAdvertisements({ state, commit }) {
            try {
                commit('SET_LOADING', { value: true });
                let { data } = await axios.get('/sales/list');
                if (data.isSuccess) {
                    commit('SET_STATE',{ paramName: 'advertisements', value: data.result });
                    commit('SET_LOADING', { value: false });
                }
            } catch (error) {

            } finally {
                commit('SET_LOADING', { value: false });
            }
        }
    }
};