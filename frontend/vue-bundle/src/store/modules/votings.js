import axios from '@/api';
import { SET_STATE, SET_LOADING } from '@/utils/mutations';

export default {
    namespaced: true,
    state: {
        votings: [],
        loading: false,
    },
    mutations: {
        SET_STATE,
        SET_LOADING
    },
    getters: {
        votings: state => state.votings,
        loading: state => state.loading
    },
    actions: {   
        async fetchVotings({ state, commit }) {
            try {
                commit('SET_LOADING', { value: true });
                let { data } = await axios.get('/questions/list');
                if (data.isSuccess) {
                    commit('SET_STATE',{ paramName: 'votings', value: data.result });
                    commit('SET_LOADING', { value: false });
                }
            } catch (error) {

            } finally {
                commit('SET_LOADING', { value: false });
            }
        }
    }
};