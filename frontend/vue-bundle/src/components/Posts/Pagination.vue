<template>
  <nav v-if="maxPagesCount > 1">
    <ul class="pagination" style="margin: 0 40%">
      <li
        v-show="isLeftArrowVisible"
        @click="leftArrowClick"
        class="page-item">
        <button class="page-link" aria-label="Previous">
          <span aria-hidden="true">«</span>
        </button>
      </li>
      <li        
        v-for="page in maxPagesCount"
        :key="page"
        class="page-item"
      >
        <button
          :class="activePage === page ? 'active-page-item page-link' : 'page-link'"
          @click="clickByPage(page)"
          >{{ page }}</button
        >
      </li>
      <li
        v-show="isRightArrowVisible"
        @click="rightArrowClick"
        class="page-item">
        <button class="page-link" aria-label="Next">
          <span aria-hidden="true">»</span>
        </button>
      </li>
    </ul>
  </nav>
</template>

<script>
export default {
  props: {
    countOnPage: {
      type: Number,
      default: 4
    },
    totalNumberElements: {
      type: Number,
      required: true
    }
  },
  data: () => ({
    activePage: 1,
  }),
  computed: {
    isLeftArrowVisible() {
      return this.activePage !== 1;
    },
    maxPagesCount() {
      return Math.ceil(this.totalNumberElements / this.countOnPage);
    },
    isRightArrowVisible() {
      return this.activePage !== this.maxPagesCount;
    }
  },
  methods: {
    leftArrowClick() {
      if (this.activePage > 1) {
        this.activePage--;
        this.activePageChanged(this.activePage);
      }
    },
    rightArrowClick() {
      
      if (this.activePage < this.maxPagesCount) {
        this.activePage++;
        this.activePageChanged(this.activePage);
      }
    },
    clickByPage(pageNumber) {
      if (this.activePage !== pageNumber) {
        this.activePage = pageNumber;
        this.activePageChanged(this.activePage);
      }
    },
    activePageChanged(activePage) {
      this.$emit('active-page-changed', activePage);
    }
  }
}
</script>

<style scoped>
  .active-page-item {
    background-color: #4b4a45;
    border-color: #4b4a45;
    color: white
  }
</style>