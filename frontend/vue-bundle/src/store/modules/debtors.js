import axios from '@/api';

export default {
    namespaced: true,
    state: {
        debtors: []
    },
    getters: {
        debtors: state => state.debtors
    },
    actions: {
        async fetchDebtors({ state }) {
            try {
                let { data } = await axios.get('/debtors/list');
                if (data.isSuccess) {
                    state.debtors = data.result;
                }
            } catch (error) {

            }
        }
    }
};