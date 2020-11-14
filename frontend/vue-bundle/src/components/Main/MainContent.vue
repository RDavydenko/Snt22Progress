<template>
    <div id="wrapper" class="main-container">        
        <div class="row mx-0">
          <Menu />
          <div class="col-sm-12 col-md-9 float-right">
            <div class="container my-3">
                <router-view />              
            </div>
          </div>
        </div>
      </div>
</template>

<script>

import Menu from '@/components/Main/MainContent/Menu.vue'

export default {
  components: {
    Menu
  }
}
</script>