<template>
  <div v-loading.sync="loading">
    <h1>Объявления о продаже участков</h1>
    <hr />
    <div v-if="!isAuth"
        class="alert alert-warning"
        role="alert"
      >
        Чтобы просматривать объявления о продаже участков, Вам нужно зарегистрироваться или войти.
    </div>
    <div v-else>
      <div v-if="advertisements.length == 0 && !loading" class="alert alert-warning">Пока что нет объявлений...</div>
      <template v-else>
          <template v-for="adv in advertisements">
            <div :key="adv.id" class="card list-sales-cards">
              <router-link :to="'/sales/' + adv.id">
                <img :src="getImageOrDefault(adv)" class="card-img-top" style='height: 230px !important;' :alt="adv.title">
                <div class="card-body">
                  <h5 class="card-title">{{ adv.title }}</h5>
                  <div class="list-sales-cards-body">
                    <p class="card-text">Кол-во соток - {{ adv.square }}</p>
                    <p class="card-text">Приватизирован - {{ adv.isPrivatizated ? 'Да' : 'Нет'}}</p>
                    <p class="card-text">Цена: {{ adv.price }} руб.</p>
                  </div>
                </div>
              </router-link>
            </div>           
          </template>
      </template>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  computed: {
    ...mapGetters('sales', ['advertisements', 'loading', 'defaultImageUrl']),
    ...mapGetters('appState', ['isAuth']),
  },
  async created() {
    if (this.isAuth) {
      if (this.advertisements.length == 0) {
        await this.$store.dispatch('sales/fetchAdvertisements');
        await this.$store.dispatch('sales/fetchDefaultImage');
      }
    }
  },
  methods: {
    getImageOrDefault(advertisement) {
      if (advertisement.image != null && advertisement.image.path != null) {
        return advertisement.image.path;
      }
      else {
        return this.defaultImageUrl;
      }
    }
  }
};
</script>

<style>
</style>