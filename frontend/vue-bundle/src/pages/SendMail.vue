<template>
  <div>
    <h1>Написать в правление</h1>
    <hr />
    <div v-if="!isAuth"
        class="alert alert-warning"
        role="alert"
      >
        Чтобы отправить письмо, Вам нужно зарегистрироваться или войти.
    </div>
    <form v-else
       @submit.prevent="submit">
      <div class="form-group">
        <div style="width: 380px" class="mt-2">
          <div><label for="title">Тема сообщения</label></div>
          <div>
            <input
              v-model="mail.title"
              class="form-control"
              placeholder="Тема сообщения"
              type="text"
            />
          </div>
        </div>
        <div style="width: 380px" class="mt-2">
          <div><label for="text">Текст сообщения</label></div>
          <div>
            <textarea
              v-model="mail.text"
              class="form-control"
              cols="5"
              placeholder="Сообщение"
              rows="10"
            ></textarea>
          </div>
        </div>
      </div>      
      <button type="submit" class="btn btn-dark btn-sm">Написать</button>
    </form>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  data: () => ({
    mail: {
      theme: '',
      text: ''
    }
  }),
  computed: {
    ...mapGetters('appState', ['isAuth'])
  },
  methods: {
    submit() {
      this.$store.dispatch('sendMail/send', this.mail);
    }
  }
};
</script>

<style>
</style>