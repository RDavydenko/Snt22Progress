<template>
  <div>
    <h1>Вход в аккаунт</h1>
    <form @submit.prevent="login">
      <div class="form-group">
        <div class="mt-2" style="width: 380px">
          <div><label for="email">Адрес электронной почты</label></div>
          <div>
            <input
              class="form-control"
              id="email"
              name="email"
              placeholder="name@mail.ru"
              type="text"
              value=""
              v-model="email"
            />
          </div>
        </div>

        <div class="mt-2" style="width: 380px">
          <div><label for="password">Пароль</label></div>
          <div>
            <input
              class="form-control"
              id="password"
              name="password"
              type="password"
              value=""
              v-model="password"
            />
          </div>
        </div>
      </div>
      <button type="submit" class="btn btn-dark btn-sm mr-3">Войти</button>
      <router-link class="mr-3" to="/registration"
        >Зарегистрироваться</router-link
      >
      <router-link to="/reset-password">Забыли пароль?</router-link>
    </form>
  </div>
</template>

<script>
import auth from "@/api/auth";
import { mapGetters } from "vuex";

export default {
  data() {
    return {
      email: "",
      password: "",
    };
  },
  computed: {
    ...mapGetters("appState", ["isAuth"])
  },
  methods: {
    async login() {
      let isSuccess = await auth.login(this.email, this.password);
      if (isSuccess) {
        await this.$store.dispatch('appState/fetchAppState');
      }
      console.log(this.isAuth);
      if (this.isAuth === true) {
        this.$router.push('/')
      }
    }
  }
};
</script>

<style>
</style>