<template>
  <div id="app" v-loading.fullscreen.sync="loading">
    <div class="container-lg">
      <!-- Big nav menu -->
      <BigNavMenu />
      <!-- Small nav menu (burger-menu) -->
      <SmallNavMenu />
      <!-- Main content -->
      <MainContent />
      <!-- footer -->
      <Footer />
    </div>
  </div>
</template>

<script>
import BigNavMenu from '@/components/Main/BigNavMenu.vue';
import SmallNavMenu from '@/components/Main/SmallNavMenu.vue';
import MainContent from '@/components/Main/MainContent.vue';
import Footer from '@/components/Main/Footer.vue';
import { mapGetters } from 'vuex';

export default {
  components: {
    BigNavMenu,
    SmallNavMenu,
    MainContent,
    Footer
  },
  computed: {
    ...mapGetters('appState', ['loading'])
  },
  async beforeCreate() {
    await this.$store.dispatch('appState/fetchAppState');
  }
}
</script>