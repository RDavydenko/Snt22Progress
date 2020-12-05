<template>
    <div class="container-xs d-md-none">
        <div class="row-xs">
          <div style="padding: 0" class="col-12 menu-top">
            <ul style="padding: 0; margin: 0" lass="nav menu-top">
              <input id="menu__toggle" type="checkbox" />
              <label
                style="height: 60px; margin: 0"
                class="menu__btn"
                for="menu__toggle"
              >
                <span style="right: 70px"></span>
              </label>
              <ul style="padding: 0" class="menu__box">
                <hr style="background-color: white; margin: 0" />
                <li>
                  <router-link
                    class="menu__item"
                    to="/"
                    active-class="active-item"
                    exact
                    ><svg
                      width="1em"
                      height="1em"
                      viewBox="0 0 20 20"
                      class="bi bi-house"
                      fill="currentColor"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      <path
                        fill-rule="evenodd"
                        d="M2 13.5V7h1v6.5a.5.5 0 0 0 .5.5h9a.5.5 0 0 0 .5-.5V7h1v6.5a1.5 1.5 0 0 1-1.5 1.5h-9A1.5 1.5 0 0 1 2 13.5zm11-11V6l-2-2V2.5a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5z"
                      />
                      <path
                        fill-rule="evenodd"
                        d="M7.293 1.5a1 1 0 0 1 1.414 0l6.647 6.646a.5.5 0 0 1-.708.708L8 2.207 1.354 8.854a.5.5 0 1 1-.708-.708L7.293 1.5z"
                      /></svg
                    >Главная</router-link
                  >
                </li>
                <li>
                  <router-link
                    class="menu__item"
                    to="/voting"
                    active-class="active-item"
                    ><svg
                      width="1em"
                      height="1em"
                      viewBox="0 0 20 20"
                      class="bi bi-bar-chart-line"
                      fill="currentColor"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      <path
                        fill-rule="evenodd"
                        d="M4 11H2v3h2v-3zm5-4H7v7h2V7zm5-5h-2v12h2V2zm-2-1a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h2a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1h-2zM6 7a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1v7a1 1 0 0 1-1 1H7a1 1 0 0 1-1-1V7zm-5 4a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1v3a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1v-3z"
                      />
                      <path
                        fill-rule="evenodd"
                        d="M0 14.5a.5.5 0 0 1 .5-.5h15a.5.5 0 0 1 0 1H.5a.5.5 0 0 1-.5-.5z"
                      /></svg
                    >Голосование</router-link
                  >
                </li>
                <li>
                  <router-link
                    class="menu__item"
                    to="/send-mail"
                    active-class="active-item"
                    ><svg
                      width="1em"
                      height="1em"
                      viewBox="0 0 20 20"
                      class="bi bi-chat-square-text"
                      fill="currentColor"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      <path
                        fill-rule="evenodd"
                        d="M14 1H2a1 1 0 0 0-1 1v8a1 1 0 0 0 1 1h2.5a2 2 0 0 1 1.6.8L8 14.333 9.9 11.8a2 2 0 0 1 1.6-.8H14a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1zM2 0a2 2 0 0 0-2 2v8a2 2 0 0 0 2 2h2.5a1 1 0 0 1 .8.4l1.9 2.533a1 1 0 0 0 1.6 0l1.9-2.533a1 1 0 0 1 .8-.4H14a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"
                      />
                      <path
                        fill-rule="evenodd"
                        d="M3 3.5a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9a.5.5 0 0 1-.5-.5zM3 6a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9A.5.5 0 0 1 3 6zm0 2.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5z"
                      /></svg
                    >Написать в правление</router-link
                  >
                </li>
                <li>
                  <router-link
                    class="menu__item"
                    to="/contacts"
                    active-class="active-item"
                    ><svg
                      width="1em"
                      height="1em"
                      viewBox="0 0 20 20"
                      class="bi bi-person-lines-fill"
                      fill="currentColor"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      <path
                        fill-rule="evenodd"
                        d="M1 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H1zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm7 1.5a.5.5 0 0 1 .5-.5h2a.5.5 0 0 1 0 1h-2a.5.5 0 0 1-.5-.5zm-2-3a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 0 1h-4a.5.5 0 0 1-.5-.5zm0-3a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 0 1h-4a.5.5 0 0 1-.5-.5zm2 9a.5.5 0 0 1 .5-.5h2a.5.5 0 0 1 0 1h-2a.5.5 0 0 1-.5-.5z"
                      /></svg
                    >Контакты</router-link
                  >
                </li>
                <li v-if="!isAuth">
                  <router-link
                    style="text-decoration: underline"
                    class="menu__item"
                    active-class="active-item"
                    to="/login"
                    >Войти</router-link
                  >
                </li>
                <template v-else>
                  <li>
                    <router-link
                      class="menu__item"
                      to="/cabinet"
                      active-class="active-item"
                      ><svg
                        width="1em"
                        height="1em"
                        viewBox="0 0 20 20"
                        class="bi bi-person"
                        fill="currentColor"
                        xmlns="http://www.w3.org/2000/svg"
                      >
                        <path
                          fill-rule="evenodd"
                          d="M13 14s1 0 1-1-1-4-6-4-6 3-6 4 1 1 1 1h10zm-9.995-.944v-.002.002zM3.022 13h9.956a.274.274 0 0 0 .014-.002l.008-.002c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664a1.05 1.05 0 0 0 .022.004zm9.974.056v-.002.002zM8 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4zm3-2a3 3 0 1 1-6 0 3 3 0 0 1 6 0z"
                        /></svg
                      >Личный кабинет</router-link
                    >
                  </li>
                  <li>
                    <a
                      style="text-decoration: underline"
                      class="menu__item"
                      @click="logout"
                      ><svg
                        width="1em"
                        height="1em"
                        viewBox="0 0 20 17"
                        class="bi bi-door-open"
                        fill="currentColor"
                        xmlns="http://www.w3.org/2000/svg"
                      >
                        <path
                          fill-rule="evenodd"
                          d="M1 15.5a.5.5 0 0 1 .5-.5h13a.5.5 0 0 1 0 1h-13a.5.5 0 0 1-.5-.5zM11.5 2H11V1h.5A1.5 1.5 0 0 1 13 2.5V15h-1V2.5a.5.5 0 0 0-.5-.5z"
                        />
                        <path
                          fill-rule="evenodd"
                          d="M10.828.122A.5.5 0 0 1 11 .5V15h-1V1.077l-6 .857V15H3V1.5a.5.5 0 0 1 .43-.495l7-1a.5.5 0 0 1 .398.117z"
                        />
                        <path
                          d="M8 9c0 .552.224 1 .5 1s.5-.448.5-1-.224-1-.5-1-.5.448-.5 1z"
                        /></svg
                      >Выйти
                      </a>
                  </li>
                </template>
                <hr style="background-color: white; margin: 0" />
                <li>
                  <router-link
                    class="menu__item"
                    to="/docs"
                    active-class="active-item"
                    >Документы</router-link
                  >
                </li>
                <li>
                  <router-link
                    class="menu__item"
                    to="/government"
                    active-class="active-item"
                    >Правление</router-link
                  >
                </li>
                <li>
                  <router-link
                    class="menu__item"
                    to="/plan-scheme"
                    active-class="active-item"
                    >План-схема СНТ</router-link
                  >
                </li>
                <li>
                  <router-link
                    class="menu__item"
                    to="/legislation"
                    active-class="active-item"
                    >Законодательство</router-link
                  >
                </li>
                <li>
                  <router-link
                    class="menu__item"
                    to="/debtors"
                    active-class="active-item"
                    >Должники</router-link
                  >
                </li>
                <hr style="background-color: white; margin: 0" />
              </ul>
            </ul>
          </div>
        </div>
      </div>
</template>

<script>
import { mapGetters } from 'vuex';
import { SetAuthorizationToken } from '@/api/index';

export default {
  computed: {
    ...mapGetters('appState', ['isAuth'])
  },
  methods: {
    async logout() {
      localStorage.removeItem('authToken');
      SetAuthorizationToken('empty');
      await this.$store.dispatch('appState/fetchAppState');
      this.$router.push('/')
    }
  }
}
</script>

<style scoped>
  .active-item {
    color: #000
  }
  a:not([href]) {
    color: #fff;
  }
</style>