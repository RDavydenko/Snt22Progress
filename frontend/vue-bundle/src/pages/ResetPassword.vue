<template>
  <div>
    <h1>Забыли свой пароль?</h1>
    <hr />
    <p>Введите свой адрес электронной почты, чтобы получить новый пароль.</p>
    <form action="" method="post">
      <div class="form-group">
        <div class="mt-2" style="width: 380px">
          Адрес электронной почты
          <input
            type="email"
            name="email"
            autocomplete="email"
            maxlength="254"
            required=""
            id="id_email"
          />
        </div>
      </div>
      <button type="submit" class="btn btn-dark btn-sm mr-3">
        Отправить письмо
      </button>
      <router-link class="mr-3" to="/registration">Зарегистрироваться</router-link>
      <router-link to="/login">Войти</router-link>      
    </form>
  </div>
</template>

<script>
export default {};
</script>

<style>
</style>