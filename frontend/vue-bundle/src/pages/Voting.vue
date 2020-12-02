<template>
  <div>
    <h1>Голосование</h1>
    <hr />

    <div v-if="votings.length === 0" class="alert alert-warning" role="alert">
      Голосование скоро появится.
    </div>
    <template v-else>
      <template v-for="voting in votings">
        <div :key="voting.id">

        </div>
      </template>
    </template>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  computed: {
    ...mapGetters('votings', ['votings'])
  },
  async beforeCreate() {
    await this.$store.dispatch('votings/fetchVotings');
  }
};
</script>

<style>
</style>