<template>
  <div>
    <h1>Законодательство</h1>
    <hr />
    <template v-for="l in legislations">
      <div :key="l.id">
        <a target="_blank" :href="l.url">{{ l.text }}</a>
        <hr />
      </div>
    </template>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  computed: {
    ...mapGetters("legislation", ["legislations"]),
  },
  async beforeCreate() {
    this.$store.dispatch("legislation/fetchLegislations");
  },
};
</script>

<style>
</style>