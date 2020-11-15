<template>
  <div>
    <h1>Документы</h1>
    <hr />
    <FotoramaSlider>
      <template>
        <img src="@/assets/images/docs/Plan_rabot_na_2019_god.jpg" />
      </template>
    </FotoramaSlider>
  </div>
</template>

<script>
import FotoramaSlider from "@/components/Slider/FotoramaSlider.vue";

export default {
  components: {
    FotoramaSlider,
  },
};
</script>