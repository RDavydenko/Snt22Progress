<template>
  <div>
    <h1>Должники</h1>
    <hr />
    <FotoramaSlider>
      <template>
        <img 
          v-for="debtor in debtors"
          :key="debtor.id"
          :src="debtor.path.replace('\\', '/')" />
      </template>
    </FotoramaSlider>
  </div>
</template>

<script>
import FotoramaSlider from "@/components/Slider/FotoramaSlider.vue";
import { mapGetters } from 'vuex';

export default {
  components: {
    FotoramaSlider,
  },
  computed: {
    ...mapGetters('debtors', ['debtors'])
  },
  async beforeCreate() {
    await this.$store.dispatch('debtors/fetchDebtors');
  }
};
</script>