<template>
  <div class="col-md-2 col-sm-12 mx-4 py-1">
    <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
      <div class="card small-pic bg-dark text-white">
        <img
          v-if="vault.isPrivate == false"
          src="https://gangsterreport.com/wp-content/uploads/2018/03/adh.jpg"
          class="img-fluid"
          alt="..."
        />
        <img
          v-else
          src="https://www.jewelersmutual.com/sites/default/files/2021-08/different-types-of-vaults-blog.png"
          class="img-fluid"
          alt="..."
        />
        <div class="card-img-overlay">
          <h5 class="card-title">{{ vault.name }}</h5>
          <p class="card-text">
            {{ vault.description }}
          </p>
        </div>
      </div>
    </router-link>
  </div>
</template>


<script>
import { computed, onMounted, watchEffect } from "@vue/runtime-core"
import Pop from "../utils/Pop"
import { AppState } from "../AppState"
import { vaultKeepsService } from "../services/VaultKeepsService"
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    onMounted(async () => {
      try {
        AppState.vaultKeeps = []
        AppState.vaultKeep = {}
        await vaultKeepsService.getKeepsByVaultId(props.vault.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      vaultKeeps: computed(() => AppState.vaultKeeps)
    }
  }
}
</script>
<style scoped lang="scss">
.small-pic {
  height: 10rem;
  width: 15rem;
}
</style>