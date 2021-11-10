<template>
  <div class="container-fluid bg-dark tall">
    <div class="row">
      <div class="col-12">
        <h1>{{ vault.name }}</h1>
        <h1>{{ vaultKeeps.length }}</h1>
        <div>
          <button
            @click="deleteVault(vault.id, vault.creatorId)"
            class="btn btn-dark text-light"
          >
            Delete Vault
          </button>
        </div>
      </div>
    </div>
    <div class="row" v-if="vaultKeeps">
      <VaultKeepCard v-for="v in vaultKeeps" :key="v.id" :vaultKeep="v" />
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { onMounted, watchEffect } from "@vue/runtime-core"
import { useRoute, useRouter } from "vue-router"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { vaultsService } from "../services/VaultsService"
import { router } from "../router"
export default {
  name: 'VaultPage',
  setup() {
    // const editable = ref({vault})
    const route = useRoute()
    const router = useRouter()
    onMounted(async () => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await vaultKeepsService.getKeepsByVaultId(route.params.id)
        await vaultsService.getVaultById(route.params.id)

      } catch (error) {
        Pop.toast(error.message, 'error')
        router.push({ name: 'Home' })
        logger.log(error)
      }
    })
    watchEffect(async () => {
      AppState.vaults = []
      await vaultsService.getVaultById(route.params.id)
    })
    return {
      router,
      route,
      async deleteVault(vaultId, creatorId) {
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(vaultId)
            console.log(creatorId)
            Pop.toast('vault has been deleted')
            router.push({
              name: 'Profile',
              params: { id: creatorId }
            })
          }
        } catch (error) {
        }
      },

      vault: computed(() => AppState.vault),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.tall {
  min-height: 43rem;
}
</style>