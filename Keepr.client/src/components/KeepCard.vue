<template>
  <div v-if="keep.creator">
    <div class="m-2">
      <div
        @click.prevent="updateKeep(keep.id, keep)"
        class="card bg-dark text-white selectable"
      >
        <img class="card-img" :src="keep.img" alt="Card image" />
        <div class="card-img-overlay d-flex flex-column">
          <div class="mt-auto">{{ keep.name }}</div>
          <router-link
            @click.stop
            :to="{ name: 'Profile', params: { id: keep.creatorId } }"
          >
            <img class="small-pic" :src="keep.creator.picture" alt="" />
          </router-link>
        </div>
      </div>
    </div>
    <Modal :id="'keep-' + keep.id">
      <template #modal-title> </template>
      <template #modal-body>
        <div>
          <div class="row">
            <div class="col-6">
              <img class="img-fluid" :src="keep.img" alt="" />
            </div>
            <div class="col-6">
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
                  <ul
                    class="dropdown-menu"
                    aria-labelledby="dropdownMenuButton1"
                  >
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
                  <router-link
                    @click.stop="goToPage()"
                    :to="{ name: 'Profile', params: { id: keep.creatorId } }"
                  >
                    <img
                      class="small-picture"
                      :src="keep.creator.picture"
                      alt=""
                    />
                  </router-link>
                </div>
                <div>{{ keep.creator.name }}</div>
              </footer>
            </div>
          </div>
        </div>
      </template>
    </Modal>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { onMounted } from "@vue/runtime-core"
import { vaultsService } from "../services/VaultsService"
import { useRouter } from "vue-router"
import { Modal } from "bootstrap"
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {

    const router = useRouter()
    return {
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            const editModal = Modal.getOrCreateInstance(document.getElementById(`keep-${props.keep.id}`))
            editModal.hide()
            await keepsService.removeKeep(props.keep.id)

            Pop.toast('keep has been deleted')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async goToPage() {
        try {
          const editModal = Modal.getOrCreateInstance(document.getElementById(`keep-${props.keep.id}`))
          editModal.hide()

        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async getVaultsByProfileId(accountId) {
        await vaultsService.getVaultsByProfileId(accountId)
      },
      async updateKeep(keepId, keep) {
        const editModal = Modal.getOrCreateInstance(document.getElementById(`keep-${props.keep.id}`))
        editModal.show()
        await keepsService.updateViewCount(keepId, keep)
      },


      account: computed(() => AppState.account),
      vaults: computed(() => AppState.userVaults),
      profile: computed(() => AppState.profile)
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
.in-line {
  display: flex;
  justify-content: space-between;
}
.at-bottom {
  display: flex;
  align-items: flex-end;
}
.small-picture {
  height: 50px;
  width: 50px;
  border-radius: 50%;
}
</style>