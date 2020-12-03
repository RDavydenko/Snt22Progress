<template>
  <div>
    <h1>Регистрация</h1>
    <hr />
    <form @submit.prevent="register">
      <div class="form-group">
        <div class="mt-2" style="width: 425px">
          <div><label for="first_name">Имя</label></div>
          <div>
            <input
              class="form-control"
              type="text"
              v-model="formOptions.fName"
            />
          </div>
        </div>

        <div class="mt-2" style="width: 425px">
          <div><label for="last_name">Фамилия</label></div>
          <div>
            <input
              class="form-control"
              type="text"
              v-model="formOptions.lName"
            />
          </div>
        </div>

        <div class="mt-2" style="width: 425px">
          <div><label for="location">Номер участка</label></div>
          <div>
            <input
              class="form-control"
              type="text"
              v-model="formOptions.areaNumber"
            />
          </div>
        </div>

        <div class="mt-2" style="width: 425px">
          <div><label for="email">Адрес электронной почты</label></div>
          <div>
            <input
              class="form-control"
              type="text"
              v-model="formOptions.email"
            />
          </div>
        </div>

        <div class="mt-2" style="width: 425px">
          <div><label for="password">Пароль</label></div>
          <div>
            <input
              class="form-control"
              type="password"
              v-model="formOptions.password"
            />
          </div>
        </div>
        
        <div class="mt-2" style="width: 425px">
          <div>
            <input class="btn btn-primary" type="submit"
            value="Зарегистрироваться"
            />
          </div>
        </div>
      </div>
      
      <router-link class="mr-3" to="/login">Войти в аккаунт</router-link>
      <router-link to="/reset-password">Забыли пароль?</router-link>
    </form>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  computed: {
    ...mapGetters('registration', ['formOptions'])
  },
  methods: {
    async register() {
      let success = await this.$store.dispatch('registration/register');
      location.replace('/login');
    }
  }
};
</script>

<style>
</style>