<template>
  <div id="top" v-loading.sync="loading">
    <hr />
    <div class="card" style="width: 100%">
      <div class="card text-center">
        <div class="card-header">
          <router-link style="float: left" to="/sales">Назад</router-link>
        </div>
      </div>
      <h2>{{ activeSaleItem.title }}</h2>

      <div
        class="fotorama"
        data-maxheight="100%"
        data-maxwidth="100%"
        data-loop="true"
        data-nav="thumbs"
        data-thumbwidth="40"
        data-thumbheight="40"
        data-transition="crossfade"
        data-allowfullscreen="true"
        data-arrows="always"
        data-swipe="true"
        data-shadows="true"
      >
        <img
          :src="getImageOrDefault(activeSaleItem)"
          class="card-img-top"
          :alt="activeSaleItem.title"
        />
      </div>
      <div class="card-body">
        <p class="card-text">{{ activeSaleItem.text }}</p>
        <hr />
        <p class="card-text">Кол-во соток - {{ activeSaleItem.square }}</p>
        <p class="card-text">
          Приватизирован - {{ activeSaleItem.isPrivatizated ? 'Да' : 'Нет' }}
        </p>
        <hr />
        <p class="card-text" style="font-weight: bold; display: inline-block">
          Цена: {{ activeSaleItem.price }} руб.
          <span style="position: absolute; right: 0; margin-right: 50px"
            >Номер телефона: {{ activeSaleItem.phone }}</span
          >
        </p>
      </div>
      <div class="card text-center">
        <div class="card-header">
          <a style="float: left" href="#top">Наверх</a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  computed: {
    ...mapGetters("sales", ["activeSaleItem", "loading", "defaultImageUrl"]),
    saleId() {
      return this.$route.params.id;
    },
  },
  async created() {
    await this.fetchSale();
  },
  watch: {
    $route: "fetchSale",
  },
  methods: {
    async fetchSale() {
      this.$store.dispatch("sales/clearActiveSaleItem");
      await this.$store.dispatch("sales/fetchActiveSaleItem", this.saleId);
      if (this.defaultImageUrl == null || this.defaultImageUrl == '') {
        await this.$store.dispatch("sales/fetchDefaultImage");
      }
    },
    getImageOrDefault(sale) {
      if (sale.image != null && sale.image.path != null) {
        return sale.image.path;
      } else {
        return this.defaultImageUrl;
      }
    },
  },
};
</script>

<style>
</style>