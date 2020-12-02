import axios from '@/api';

export default {
    namespaced: true,
    state: {
        members: []
    },
    getters: {
        members: state => state.members
    },
    actions: {
        async fetchMembers({ state }) {
            try {
                let { data } = await axios.get('/government/list');
                if (data.isSuccess) {
                    state.members = data.result;
                }
            } catch (error) {

            }
        }
    }
};