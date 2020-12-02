<template>
  <div>
    <PostMiniItem
      v-for="post in posts"
      :key="post.id"
      :post="post"
    />
    <Pagination />
  </div>
</template>

<script>
import PostMiniItem from '@/components/Posts/PostMiniItem.vue';
import Pagination from '@/components/Posts/Pagination.vue';
import { mapGetters } from 'vuex';

export default {
    components: {
        PostMiniItem,
        Pagination
    },
    computed: {
      ...mapGetters('posts', ['posts'])
    },
    async beforeCreate() {
      await this.$store.dispatch('posts/fetchPosts');
    }
};
</script>

<style>
</style>