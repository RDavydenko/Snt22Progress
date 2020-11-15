<template>
  <nav aria-label="Page navigation example">
    <ul class="pagination" style="margin: 0 40%">
      <li
        style="background-color: #4b4a45; border-color: #4b4a45"
        class="page-item"
      >
        <a
          style="background-color: #4b4a45; border-color: #4b4a45; color: white"
          class="page-link"
          href="?page=1"
          >1</a
        >
      </li>

      <li class="page-item"><a class="page-link" href="?page=2">2</a></li>

      <li class="page-item">
        <a class="page-link" href="?page=2" aria-label="Next">
          <span aria-hidden="true">»</span>
        </a>
      </li>
    </ul>
  </nav>
</template>

<script>
export default {

}
</script>

<style>

</style>