import axios from '@/api';

export default {
    namespaced: true,
    state: {
        posts: [],
        activePost: {},
        loading: false
    },
    getters: {
        posts: state => state.posts,
        activePost: state => state.activePost,
        lastFivePosts: state => state.posts.slice(-5),
        loading: state => state.loading
    },
    mutations: {
        SET_LOADING(state, value) {
            state.loading = Boolean(value);
        }
    },
    actions: {
        async fetchPosts({ state, commit }) {
            try {
                commit('SET_LOADING', true);
                let { data } = await axios.get('/posts/list');
                if (data.isSuccess) {
                    state.posts = data.result;
                    commit('SET_LOADING', false);
                }
            } catch (error) {

            } finally {
                commit('SET_LOADING', false);
            }
        },
        async fetchPostById({ state, commit }, postId) {
            try {
                commit('SET_LOADING', true);
                let { data } = await axios.get(`/posts/${postId}`);
                if (data.isSuccess) {
                    state.activePost = data.result;
                    commit('SET_LOADING', false);
                }
            } catch (error) {

            } finally {
                commit('SET_LOADING', false);
            }
        }
    }
};