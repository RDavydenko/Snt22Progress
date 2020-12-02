<template>
  <ul style="list-style: none; padding-left: 0; margin: 0">
        <li v-for="post in lastFivePosts"
            :key="post.id"
            class="news__item" style="width: 100%; text-align: center">
          <router-link
            style="
              display: block;
              text-decoration: none;
              color: black;
              padding: 5px;
            "
            :to="`/${post.id}`"
          >
            {{ post.title }}
          </router-link>
        </li>
      </ul>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
    computed: {
        ...mapGetters('posts', ['lastFivePosts'])
    },
    async beforeCreate() {
        await this.$store.dispatch('posts/fetchPosts');
    }
}
</script>
