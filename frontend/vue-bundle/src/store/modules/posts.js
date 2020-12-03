import axios from '@/api';
import { SET_STATE, SET_LOADING } from '@/utils/mutations';

export default {
    namespaced: true,
    state: {
        posts: [],
        activePost: {},
        activePagePosts: [],
        total: 0,
        loading: false
    },
    getters: {
        posts: state => state.posts,
        activePost: state => state.activePost,
        lastFivePosts: state => state.posts.slice(-5),
        activePagePosts: state => state.activePagePosts,
        total: state => state.total,
        loading: state => state.loading
    },
    mutations: {
        SET_STATE,
        SET_LOADING
    },
    actions: {
        async fetchPosts({ state, commit }) {
            try {
                commit('SET_LOADING', { value: true });
                let { data } = await axios.get('/posts/list');
                if (data.isSuccess) {
                    commit('SET_STATE', { paramName: 'posts', value: data.result });
                    commit('SET_LOADING', { value: false });
                }
            } catch (error) {

            } finally {
                commit('SET_LOADING', { value: false });
            }
        },
        async fetchPostById({ state, commit }, postId) {
            try {
                commit('SET_LOADING', { value: true });
                let { data } = await axios.get(`/posts/${postId}`);
                if (data.isSuccess) {
                    commit('SET_STATE', { paramName: 'activePost', value: data.result });
                    commit('SET_LOADING', { value: false });
                }
            } catch (error) {

            } finally {
                commit('SET_LOADING', { value: false });
            }
        },
        async fetchPagingPosts({ state, commit }, { pageNumber, pageSize }) {
            try {
                commit('SET_LOADING', { value: true });
                let { data } = await axios.get(`/posts/page/${pageNumber}?pageSize=${pageSize}`);
                if (data.isSuccess) {
                    commit('SET_STATE', { paramName: 'total', value: data.result.total });
                    commit('SET_STATE', { paramName: 'activePagePosts', value: data.result.items });
                    commit('SET_LOADING', { value: false });
                }
            } catch (error) {

            } finally {
                commit('SET_LOADING', { value: false });
            }
        }
    }
};