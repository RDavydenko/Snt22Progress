import axios from '@/api';

export default {
    namespaced: true,
    state: {
        documents: []
    },
    getters: {
        documents: state => state.documents
    },
    actions: {
        async fetchDocuments({ state }) {
            try {
                let { data } = await axios.get('/documents/list');
                if (data.isSuccess) {
                    state.documents = data.result;
                }
            } catch (error) {

            }
        }
    }
};