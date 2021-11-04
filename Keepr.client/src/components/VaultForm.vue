<template>
  <form @submit.prevent="postVault()">
    <div class="form-group">
      <label for="name" class="sr-only"></label>
      <input
        type="text"
        name="name"
        class="form-control"
        placeholder="Name..."
        required
        v-model="editable.name"
      />
    </div>
    <div class="form-group">
      <label for="description" class="sr-only"></label>
      <input
        type="text"
        name="description"
        class="form-control"
        placeholder="Description"
        required
        v-model="editable.description"
      />
    </div>
    <div class="form-group">
      <label for="isPrivate">Make Private </label>
      <input
        type="checkbox"
        class="ms-3 mt-3"
        name="isPrivate"
        v-model="editable.isPrivate"
      />
    </div>
    <button type="submit" class="btn btn-success">SAVE</button>
  </form>
</template>
<script>

import { ref, watchEffect } from "@vue/runtime-core"
import { AppState } from "../AppState"
import { vaultsService } from "../services/VaultsService"
import { Modal } from "bootstrap"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
export default {
  setup() {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...AppState.vault }
    })
    return {
      editable,
      async postVault() {
        try {
          vaultsService.postVault(editable.value)
          const modal = Modal.getOrCreateInstance(document.getElementById("add-vault"))
          modal.hide()
          Pop.toast("this was posted")
        } catch (error) {
          Pop.toast(error.message, 'error')
          logger.log(error)

        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>