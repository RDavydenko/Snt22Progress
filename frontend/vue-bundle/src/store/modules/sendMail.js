import axios from '@/api';
import { SET_LOADING } from '@/utils/mutations';
import { showSuccessNotify, showErrorNotify } from '@/utils/notify';

export default {
    namespaced: true,
    state: {
        loading: false
    },
    mutations: {
        SET_LOADING
    },
    actions: {
        async send({ commit }, mail) {
            try {
                commit('SET_LOADING', { value: true });
                let { data } = await axios.post('/mail/send', JSON.stringify(mail));
                if (data.isSuccess) {
                    commit('SET_LOADING', { value: false });
                    showSuccessNotify('Письмо успешно отправлено!');
                }
                else {
                    showErrorNotify('Письмо не было отправлено');
                }
            } catch (error) {
                showErrorNotify('Возникла ошибка при отправке письма');
            } finally {
                commit('SET_LOADING', { value: false });
            }
        }
    }
};