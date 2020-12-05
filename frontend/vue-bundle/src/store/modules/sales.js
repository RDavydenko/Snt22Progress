import axios from '@/api';
import { SET_STATE, SET_LOADING } from '@/utils/mutations'

export default {
    namespaced: true,
    state: {
        advertisements: [],
        loading: false,
        defaultImageUrl: '',
        activeSaleItem: {}
    },
    getters: {
        advertisements: state => state.advertisements,
        loading: state => state.loading,
        defaultImageUrl: state => state.defaultImageUrl,
        activeSaleItem: state => state.activeSaleItem,
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
                    commit('SET_STATE', { paramName: 'advertisements', value: data.result });
                    commit('SET_LOADING', { value: false });
                }
            } catch (error) {

            } finally {
                commit('SET_LOADING', { value: false });
            }
        },
        async fetchDefaultImage({ commit }) {
            try {
                let { data } = await axios.get('/sales/get-default-image');
                if (data.isSuccess) {
                    commit('SET_STATE', { paramName: 'defaultImageUrl', value: data.result });
                }
            } catch (error) {

            } finally {
                
            }
        },
        async fetchActiveSaleItem({ commit }, saleId) {
            try {  
                commit('SET_LOADING', { value: true });
                let { data } = await axios.get(`/sales/${saleId}`);
                if (data.isSuccess) {
                    commit('SET_STATE', { paramName: 'activeSaleItem', value: data.result });
                    commit('SET_LOADING', { value: false });
                }
            } catch (error) {

            } finally {
                commit('SET_LOADING', { value: false });
            }
        },
        clearActiveSaleItem({ commit }) {
            commit('SET_STATE', { paramName: 'activeSaleItem', value: {} });
        }
    }
};