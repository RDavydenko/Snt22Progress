<template>
  <div>
    <h1>Правление</h1>
    <div style="font-size: 19px; font-weight: bolder">
      <hr />
      <template v-for="member in members">
        {{ `${member.lName} ${member.fName} ${member.mName}` }} <br :key="member.id"/>
      </template>
      <hr />
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  computed: {
    ...mapGetters('government', ['members'])
  },
  async beforeCreate() {
    await this.$store.dispatch('government/fetchMembers');
  }
};
</script>

<style>
</style>