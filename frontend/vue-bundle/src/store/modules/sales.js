import axios from '@/api';
import { SET_STATE } from '@/utils/mutations'

export default {
    namespaced: true,
    state: {
        advertisements: [],
        loading: false
    },
    getters: {
        advertisements: state => state.advertisements
    },
    mutations: {
        SET_STATE
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