import axios from '@/api';
import { SET_STATE, SET_LOADING } from '@/utils/mutations';
import { showSuccessNotify, showErrorNotify } from '@/utils/notify';

export default {
    namespaced: true,
    state: {
        votings: [],
        loading: false,
    },
    mutations: {
        SET_STATE,
        SET_LOADING,
        UPDATE_VOTING(state, voting) {
            let index = -1;
            for (let i = 0; i < state.votings.length; i++) {
               if (state.votings[i].id === voting.id) {
                    index = i;
                    break;
               }                
            }
            if (index !== -1) {
                state.votings[index] = voting;
            }
            else {
                state.votings.push(voting);
            }
        }
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
        },
        async vote({ commit }, { votingId, choiseId }) {
            try {
                commit('SET_LOADING', { value: true });
                let { data } = await axios.post(`/questions/${votingId}/vote`, choiseId);
                if (data.isSuccess) {
                    commit('UPDATE_VOTING', data.result);
                    commit('SET_LOADING', { value: false });
                    showSuccessNotify('Успешно проголосовали. Теперь этот опрос находится ниже в списке уже проголосованных');
                } else {
                    showErrorNotify('Не удалось проголосовать. Возможно, произошла ошибка');
                }
            } catch (error) {
                showErrorNotify('Произошла ошибка при голосовании');
            } finally {
                commit('SET_LOADING', { value: false });
            }
        }
    }
};