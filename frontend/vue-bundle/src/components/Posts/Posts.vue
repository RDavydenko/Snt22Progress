<template>
  <div>
    <PostMiniItem/>
    <PostMiniItem/>
    <PostMiniItem/>
    <Pagination />
  </div>
</template>

<script>
import PostMiniItem from '@/components/Posts/PostMiniItem.vue';
import Pagination from '@/components/Posts/Pagination.vue';

export default {
    components: {
        PostMiniItem,
        Pagination
    }
};
</script>

<style>
</style>