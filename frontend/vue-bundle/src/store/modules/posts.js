import axios from '@/api';

export default {
    namespaced: true,
    state: {
        posts: [],
        activePost: {}
    },
    getters: {
        posts: state => state.posts,
        activePost: state => state.activePost,
        lastFivePosts: state => state.posts.slice(-5)
    },
    actions: {
        async fetchPosts({ state }) {
            try {
                let { data } = await axios.get('/posts/list');
                if (data.isSuccess) {
                    state.posts = data.result;
                }
            } catch (error) {

            }
        },
        async fetchPostById({ state }, postId) {
            try {
                let { data } = await axios.get(`/posts/${postId}`);
                if (data.isSuccess) {
                    state.activePost = data.result;
                }
            } catch (error) {
            }
        }
    }
};