<template>
  <div id="app">
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

export default {
  components: {
    BigNavMenu,
    SmallNavMenu,
    MainContent,
    Footer
  }
}
</script>

<style lang="scss">
</style>
