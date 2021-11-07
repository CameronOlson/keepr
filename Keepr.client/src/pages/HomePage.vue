<template>
  <div class="container-fluid bg-dark">
    <div v-if="keeps" class="masonry">
      <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { AppState } from "../AppState"
import { onMounted, watchEffect } from "@vue/runtime-core"
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Home',
  setup() {
    watchEffect(async () => {
      try {
        AppState.keeps = []
        await keepsService.getKeeps()
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
// .masonry {
//   columns: 4;
//   column-gap: 2px;
// }
// .box {
//   width: 20rem;
//   padding: 2rem;
//   margin-bottom: 10px;
//   break-inside: avoid;
// }
.masonry {
  columns: 4 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
  }
}
</style>
