<template>
  <div>
    <h1>Должники</h1>
    <hr />
    <FotoramaSlider>
      <template>
        <img src="@/assets/images/debtors/debtors-1.jpg" />
        <img src="@/assets/images/debtors/debtors-2.jpg" />
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