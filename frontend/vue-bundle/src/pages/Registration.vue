<template>
  <div>
    <h1>Регистрация</h1>
    <hr />
    <form class="" action="" method="post">
      <div class="form-group">
        <div class="mt-2" style="width: 425px">
          <div><label for="first_name">Имя</label></div>
          <div>
            <input
              class="form-control"
              id="first_name"
              name="first_name"
              placeholder="Вася"
              type="text"
              value=""
            />
          </div>
        </div>

        <div class="mt-2" style="width: 425px">
          <div><label for="last_name">Фамилия</label></div>
          <div>
            <input
              class="form-control"
              id="last_name"
              name="last_name"
              placeholder="Пупкин"
              type="text"
              value=""
            />
          </div>
        </div>

        <div class="mt-2" style="width: 425px">
          <div><label for="last_name">Отчество</label></div>
          <div>
            <input
              class="form-control"
              id="last_name"
              name="last_name"
              placeholder="Пупкин"
              type="text"
              value=""
            />
          </div>
        </div>

        <div class="mt-2" style="width: 425px">
          <div><label for="location">Номер участка</label></div>
          <div>
            <input
              class="form-control"
              id="location"
              name="location"
              placeholder="1-20000"
              type="text"
              value=""
            />
          </div>
        </div>

        <div class="mt-2" style="width: 425px">
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

        <div class="mt-2" style="width: 425px">
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
      <router-link class="mr-3" to="/login">Войти в аккаунт</router-link>
      <router-link to="/reset-password">Забыли пароль?</router-link>
    </form>
  </div>
</template>

<script>
export default {};
</script>

<style>
</style>