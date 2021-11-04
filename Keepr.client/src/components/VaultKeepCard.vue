<template>
  <div @click.prevent="updateVaultKeep()" class="col-sm-12 col-md-3 m-1">
    <div class="card bg-dark text-white selectable">
      <img class="card-img" :src="vaultKeep.img" alt="Card image" />
      <div class="card-img-overlay d-flex flex-column">
        <div class="mt-auto">{{ vaultKeep.name }}</div>
        <router-link
          @click.stop
          :to="{ name: 'Profile', params: { id: vaultKeep.creator.id } }"
        >
          <img class="small-pic" :src="vaultKeep.creator.picture" alt="" />
        </router-link>
      </div>
    </div>
  </div>
  <Modal :id="'vaultKeep-' + vaultKeep.id">
    <template #modal-title> </template>
    <template #modal-body>
      <div>
        <div class="row">
          <div class="col-6">
            <img class="img-fluid" :src="vaultKeep.img" alt="" />
          </div>
          <div class="col-6">
            <div class="in-line">
              <div>Shares: {{ vaultKeep.shares }}</div>
              <div>Views: {{ vaultKeep.views }}</div>
              <div>Keeps: {{ vaultKeep.keeps }}</div>
            </div>
            <div class="text-center">
              <h1>
                {{ vaultKeep.name }}
              </h1>
            </div>

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
                  <VaultKeepVaultItem
                    v-for="v in vaults"
                    :key="v.id"
                    :vault="v"
                    :vaultKeep="vaultKeep"
                  />
                </ul>
              </div>

              <div v-if="vaultKeep.creatorId == account.id">
                <button
                  @click.prevent="deleteVaultKeep()"
                  class="btn btn-dark text-light"
                >
                  Delete
                </button>
              </div>
            </footer>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { Modal } from "bootstrap"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/VaultsService"
import { onMounted } from "@vue/runtime-core"
export default {
  props: {
    vaultKeep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    onMounted(async () => {
      AppState.vaults = []
      await vaultsService.getVaultsByProfileId(AppState.account.id)
    })

    return {
      async deleteVaultKeep() {
        try {
          if (await Pop.confirm()) {

            const editModal = Modal.getOrCreateInstance(document.getElementById(`vaultKeep-${props.vaultKeep.id}`))
            debugger
            editModal.hide()
            await vaultKeepsService.removeVaultKeep(props.vaultKeep.vaultKeepId)

            Pop.toast('keep has been deleted')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async updateVaultKeep() {
        const editModal = Modal.getOrCreateInstance(document.getElementById(`vaultKeep-${props.vaultKeep.id}`))
        editModal.show()
      },
      async getVaultsByProfileId(accountId) {
        await vaultsService.getVaultsByProfileId(accountId)
      },
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.userVaults)
    }
  }
}
</script>


<style lang="scss" scoped>
.wide {
  width: 70rem;
}
.small-pic {
  height: 30px;
  width: 30px;
  border-radius: 50%;
}
</style>