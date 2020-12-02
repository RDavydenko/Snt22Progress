<template>
  <div>
    <h1>Личный кабинет</h1>
    <form @submit.prevent="update">
      <div class="form-group">
        <div style="width: 380px" class="mt-2">
          <div><label for="first_name">Имя</label></div>
          <div>
            <input
              class="form-control"
              type="text"
              v-model="user.fName"
            />
          </div>
        </div>

        <div style="width: 380px" class="mt-2">
          <div><label for="last_name">Фамилия</label></div>
          <div>
            <input
              class="form-control"
              type="text"
              v-model="user.lName"
            />
          </div>
        </div>

        <div style="width: 380px" class="mt-2">
          <div><label for="middle_name">Отчество</label></div>
          <div>
            <input
              class="form-control"
              type="text"
              v-model="user.mName"
            />
          </div>
        </div>

        <div style="width: 380px" class="mt-2">
          <div><label for="location">Номер участка</label></div>
          <div>
            <input
              class="form-control"
              type="text"
              v-model="user.areaNumber"
            />
          </div>
        </div>

        <div style="width: 380px" class="mt-2">
          <div><label for="email">Адрес электронной почты</label></div>
          <div>
            <input
              class="form-control"
              type="text"
              v-model="user.email"
            />
          </div>
        </div>
      </div>
      <p><router-link to="/reset-password/">Забыли пароль?</router-link></p>
      <button type="submit" class="btn btn-dark btn-sm">Изменить</button>
    </form>
  </div>
</template>

<script>
import {mapGetters} from 'vuex';

export default {
  computed: {
    ...mapGetters('cabinet', ['user'])
  },
  methods: {
    update() {
      this.$store.dispatch('cabinet/updateUser', this.user);
    }
  },
  created() {
    this.$store.dispatch('cabinet/fetchUser');
  }
};
</script>