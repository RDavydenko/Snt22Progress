<template>
  <div>
    <h1>Вход в аккаунт</h1>
    <form class="" action="" method="post">
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
export default {};
</script>

<style>
</style>