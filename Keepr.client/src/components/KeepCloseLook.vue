<template>
  <div>
    <div class="row">
      <div class="col-md-6 col-sm-12">
        <img class="img-fluid" :src="keep.img" alt="" />
      </div>
      <div class="col-md-6 col-sm-12">
        <div class="in-line">
          <div>Shares: {{ keep.shares }}</div>
          <div>Views: {{ keep.views }}</div>
          <div>Keeps: {{ keep.keeps }}</div>
        </div>
        <div class="text-center">
          <h1>
            {{ keep.name }}
          </h1>
        </div>
        <p>{{ keep.description }}</p>

        <footer class="at-bottom">
          <div class="dropdown">
            <button
              @click.prevent="getVaultsByProfileId(account.id)"
              class="btn btn-secondary dropdown-toggle"
              type="button"
              id="dropdownMenuButton1"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              Add To Vault
            </button>
            <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
              <VaultItem
                v-for="v in vaults"
                :key="v.id"
                :vault="v"
                :keep="keep"
              />
            </ul>
          </div>

          <div v-if="keep.creatorId == account.id">
            <button
              @click.prevent="deleteKeep()"
              class="btn btn-dark text-light"
            >
              Delete
            </button>
          </div>
          <div></div>
          <div>
            <img class="small-picture" :src="keep.creator.picture" alt="" />
          </div>
          <div>{{ keep.creator.name }}</div>
        </footer>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
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