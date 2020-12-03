<template>
  <div v-loading.sync="loading">
    <h1>Голосование</h1>
    <hr />

    <div v-if="votings.length === 0 && !loading" class="alert alert-warning" role="alert">
      Голосование скоро появится.
    </div>
    <template v-else>
      <template v-for="voting in votings">
        <div :key="voting.id">
            {{ 'Голосование: ' + voting.text }}
        </div>
      </template>
    </template>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  computed: {
    ...mapGetters('votings', ['votings', 'loading'])
  },
  async beforeCreate() {
    await this.$store.dispatch('votings/fetchVotings');
  }
};
</script>

<style>
</style>