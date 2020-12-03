<template>
  <div class="card text-center">
    <div class="card-header" id="top">      
      <router-link style="float: left" to="/" exact>Назад</router-link>
    </div>
    <div class="card-body">
      <h5 class="card-title">{{ activePost.title }}</h5>
      <p class="card-text">
        {{ activePost.text }}
      </p>
    </div>
    <div class="card-footer text-muted">
      <a style="float: left" href="#top">Наверх</a>
      <span style="float: right">{{ activePost.created }}</span>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  data() {
    return {
      post: null
    };
  },
  computed: {
    ...mapGetters('posts', ['activePost']),
    postId() {
      return this.$route.params.id;
    }
  },
  async mounted() {
    await this.$store.dispatch('posts/fetchPostById', this.postId);
  }
};
</script>

<style>
</style>