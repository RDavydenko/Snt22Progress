import axios from '@/api';

export default {
    namespaced: true,
    state: {
        votings: []
    },
    getters: {
        votings: state => state.votings
    },
    actions: {        
        async fetchVotings({ state }) {
            try {
                let { data } = await axios.get('/questions/list');
                if (data.isSuccess) {
                    state.votings = data.result;
                }
            } catch (error) {

            }
        }
    }
};