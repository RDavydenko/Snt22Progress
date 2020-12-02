<template>
  <div>
    <h1>Документы</h1>
    <hr />
    <FotoramaSlider>
      <template>
        <img v-for="doc in documents" :key="doc.id" 
          :src="doc.path.replace('\\', '/')" />
      </template>
    </FotoramaSlider>
  </div>
</template>

<script>
import FotoramaSlider from "@/components/Slider/FotoramaSlider.vue";
import { mapGetters } from 'vuex';

export default {
  computed: {
    ...mapGetters('documents', ['documents'])
  },
  components: {
    FotoramaSlider,
  },
  async beforeCreate() {
    await this.$store.dispatch('documents/fetchDocuments');
  }
};
</script>