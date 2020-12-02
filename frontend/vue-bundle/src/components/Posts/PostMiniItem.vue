<template>
  <div class="card mb-3" style="width: 100%">
    <div class="card-body">
      <h4 class="card-title">{{ post.title }}</h4>
      <h6 class="card-subtitle mb-2 text-muted">{{ post.created }}</h6>
      <p class="card-text">
       {{ post.text }}
      </p>
      <router-link :to="`/${post.id}`">
        <button
          style="
            float: left;
            background-color: #4b4a45;
            color: white;
            font-weight: 500;
            font-size: 16px;
          "
          type="button"
          class="btn"
        >
          Читать
        </button>
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    post: {
      type: Object,
      required: true
    }
  }
}
</script>

<style>

</style>