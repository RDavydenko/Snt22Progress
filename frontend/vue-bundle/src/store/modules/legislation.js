import axios from '@/api';

export default {
    namespaced: true,
    state: {
        legislations: []
    },
    getters: {
        legislations: state => state.legislations
    },
    actions: {
        async fetchLegislations({ state }) {
            try {
                let { data } = await axios.get('/legislation/list');
                if (data.isSuccess) {
                    state.legislations = data.result;
                }
            } catch (error) {

            }
        }
    }
};