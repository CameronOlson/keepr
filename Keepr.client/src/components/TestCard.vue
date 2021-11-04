<template>
  <div class="component">
    <p>{{ keep.creator }}</p>
  </div>
</template>


<script>
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>