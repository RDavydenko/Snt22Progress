<template>
  <div v-loading.sync="loading">
    <PostMiniItem
      v-for="post in activePagePosts"
      :key="post.id"
      :post="post"
    />
    <Pagination 
      :totalNumberElements="total"
      :countOnPage="pageSize"
      @active-page-changed="activePageChanged"
    />
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
    data: () => ({
      pageSize: 4
    }),
    computed: {
      ...mapGetters('posts', ['activePagePosts', 'total', 'loading'])
    },
    async beforeCreate() {
      await this.$store.dispatch('posts/fetchPagingPosts', { pageNumber: 1, pageSize: this.pageSize })
    },
    methods: {
      async activePageChanged(activePage) {
        console.log(activePage);
        await this.$store.dispatch('posts/fetchPagingPosts', { pageNumber: activePage, pageSize: this.pageSize })
      } 
    }
};
</script>

<style>
</style>