<template>
  <div>
    <h1>Написать в правление</h1>
    <hr />
    <form class="" action="" method="post">
      <div class="form-group">
        <div style="width: 380px" class="mt-2">
          <div><label for="title">Тема сообщения</label></div>
          <div>
            <input
              class="form-control"
              id="title"
              name="title"
              placeholder="Тема сообщения"
              type="text"
              value=""
            />
          </div>
        </div>
        <div style="width: 380px" class="mt-2">
          <div><label for="text">Текст сообщения</label></div>
          <div>
            <textarea
              class="form-control"
              cols="5"
              id="text"
              name="text"
              placeholder="Сообщение"
              required=""
              rows="10"
            ></textarea>
          </div>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
export default {};
</script>

<style>
</style>