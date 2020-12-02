import axios from '@/api';

export default {
    namespaced: true,
    state: {
        advertisements: []
    },
    getters: {
        advertisements: state => state.advertisements
    },
    actions: {
        async fetchAdvertisements({ state }) {
            try {
                let { data } = await axios.get('/sales/list');
                if (data.isSuccess) {
                    state.advertisements = data.result;
                }
            } catch (error) {

            }
        }
    }
};