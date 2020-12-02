import axios from '@/api';

export default {
    namespaced: true,
    state: {
        user: {}
    },
    getters: {
        user: state => state.user
    },
    mutations: {
        setUser({ state }, value) {
            state.user = value;
        }
    },
    actions: {
        async fetchUser({ state }) {
            try {
                let { data } = await axios.get('/users/about');
                if (data.isSuccess) {
                    state.user = data.result;
                }
            } catch (error) {

            }
        },
        async updateUser({ state }, value) {
            try {
                let { data } = await axios.post('/users/edit', JSON.stringify(value));
                if (data.isSuccess) {
                    state.user = data.result;
                }
            } catch (error) {

            }
        }
    }
};