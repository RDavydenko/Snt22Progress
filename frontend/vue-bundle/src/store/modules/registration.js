import axios from '@/api';

export default {
    namespaced: true,
    state: {
        formOptions: {
            fName: '',
            lName: '',
            areaNumber: '',
            email: '',
            password: ''
        }
    },
    getters: {
        formOptions: state => state.formOptions
    },
    actions: {        
        async register({ state }) {
            try {
                let { data } = await axios.post('/users/register', JSON.stringify(state.formOptions));
                return data.isSuccess;
            } catch (error) {

            }
        }
    }
};