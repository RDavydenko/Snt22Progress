<template>
  <div>
    <h1>Объявления о продаже участков</h1>
    <hr />
    
    <div v-if="advertisements.length == 0" class="alert alert-warning">Пока что нет объявлений...</div>
    <template v-else>
        <template v-for="adv in advertisements">
          <div :key="adv.id">
            {{ 'Объявление: ' + adv.title }}
          </div>
        </template>
    </template>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  computed: {
    ...mapGetters('sales', ['advertisements'])
  },
  async beforeCreate() {
    await this.$store.dispatch('sales/fetchAdvertisements');
  }
};
</script>

<style>
</style>