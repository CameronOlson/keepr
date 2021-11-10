<template>
  <li>
    <a @click.prevent="addKeepToVault()" class="dropdown-item" href="#">{{
      vault.name
    }}</a>
  </li>
</template>



<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { keepsService } from "../services/KeepsService"
import Pop from "../utils/Pop"
export default {
  props: {
    vault: {
      type: Object,
      required: true
    },
    vaultKeep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const editable = ref({
      keepId: props.vaultKeep.id,
      vaultId: props.vault.id,

    })
    return {
      editable,
      async addKeepToVault() {
        try {
          await vaultKeepsService.postVaultKeep(editable.value)
          Pop.toast("This has been added to your vault")
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      vaults: computed(() => AppState.vaults),
      vaultKeeps: computed(() => AppState.vaultKeeps)

    }
  }
}
</script>


<style lang="scss" scoped>
</style>


