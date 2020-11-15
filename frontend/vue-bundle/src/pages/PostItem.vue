<template>
  <div class="card text-center">
    <div class="card-header" id="top">      
      <router-link style="text-align: left;" to="/" exact>Назад</router-link>
    </div>
    <div class="card-body">
      <h1>TEST: {{ postId }}</h1>
      <h5 class="card-title">Сроки оплаты</h5>
      <p class="card-text">
        Срок оплаты обязательных платежей установлен: до 1 июля - 50%, до 1
        октября - 100%. текущего года. Оплата за бытовую электроэнергию - с 15
        октября по 31 декабря текущего года На садоводов не оплативших
        обязательные платежи, установленные решением собрания, подается исковое
        заявление в суд, о принудительном взыскании имеющейся задолженности.
        Участки садоводов, не оплативших взносы, будут отключаться от
        водоснабжения и электроснабжения. Повторное подключение водо и
        энергоснабжение будет производиться за плату, установленную собранием
        садоводов.
      </p>
    </div>
    <div class="card-footer text-muted">
      <a style="float: left" href="#top">Наверх</a>
      <span style="float: right">19 сентября 2020 г. 11:55</span>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      postId: null
    };
  },
  mounted() {
    this.postId = this.$route.params.id;
  }
};
</script>

<style>
</style>