<template>
  <div class="container-fluid bg-dark">
    <div class="row pt-3">
      <div class="col-md-2 col-sm-12">
        <img :src="profile.picture" alt="" />
      </div>
      <div class="col-md-10 col-sm-12">
        <h1>
          {{ profile.name }}
          <h5>Vaults : {{ vaults.length }}</h5>
          <h5>Keeps: {{ keeps.length }}</h5>
        </h1>
      </div>
    </div>
    <div class="col-12">
      <div class="row">
        <div class="display-flex">
          <h1>Vaults</h1>

          <button
            v-if="profile.id == account.id"
            data-bs-toggle="modal"
            data-bs-target="#add-vault"
            class="btn btn-dark text-light"
          >
            Add Vault
          </button>
        </div>
      </div>
    </div>
    <div class="col-12">
      <div class="row">
        <ProfileVaultCards v-for="v in vaults" :key="v.id" :vault="v" />
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <h1>Keeps</h1>
        <button
          v-if="profile.id == account.id"
          data-bs-toggle="modal"
          data-bs-target="#add-keep"
          class="btn btn-dark text-light"
        >
          Add Keep
        </button>
      </div>
    </div>
    <div v-if="keeps" class="row">
      <div class="col masonry">
        <ProfileKeepCard v-for="k in keeps" :key="k.id" :keep="k" />
      </div>
    </div>

    <Modal id="add-keep">
      <template #modal-title>
        <h6>Post a Keep</h6>
      </template>
      <template #modal-body>
        <KeepForm />
      </template>
    </Modal>
    <Modal id="add-vault">
      <template #modal-title>
        <h6>Post a Vault</h6>
      </template>
      <template #modal-body>
        <VaultForm />
      </template>
    </Modal>
  </div>
</template>


<script>
import { computed, onMounted, watchEffect } from "@vue/runtime-core"
import { useRoute } from "vue-router"
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/VaultsService"
import { profilesService } from "../services/ProfilesService"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { AppState } from "../AppState"
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        AppState.keeps = []
        await keepsService.getKeepsByProfileId(route.params.id)
        vaultsService.getVaultsByProfileId(route.params.id)
        profilesService.getProfileById(route.params.id)
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.log(error)
      }
    })
    return {
      route,
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
    }
  }
}




</script>


<style lang="scss" scoped>
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
.for-the-row {
  display: flex;
  flex: wrap;
}
</style>