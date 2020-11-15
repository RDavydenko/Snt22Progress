<template>
  <div
    class="fotorama"
    data-maxheight="100%"
    data-maxwidth="100%"
    data-navposition="bottom"
    data-allowfullscreen="true"
    data-nav="thumbs"
    data-thumbwidth="80"
    data-thumbheight="80"
    data-transition="crossfade"
    data-arrows="always"
    data-swipe="true"
    data-shadows="true"
  >
    <slot />
  </div>
</template>

<script>
import "@/assets/slider/fotorama.css";

export default {
  mounted() {
    let script = document.createElement("script");
    script.src =
      "https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.js";
    document.documentElement.firstChild.appendChild(script);
  },
};
</script>

<style>
</style>