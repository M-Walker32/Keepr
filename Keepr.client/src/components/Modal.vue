<template>
  <div class="modal fade" id="keep-modal" tabindex="-1">
    <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-body p-0">
          <div class="container-fluid p-0">
            <div class="row m-0 p-0">
              <div class="col-md-6 col-12 m-0 p-0">
                <img class="modal-img bg-dark" :src="keep.img" />
              </div>
              <div class="col-md-6 col-12 h-100">
                <div class="row h-100">
                  <div class="col-12 d-flex justify-content-end">
                    <button
                      type="button"
                      class="btn-close mt-2"
                      data-bs-dismiss="modal"
                      aria-label="Close"
                    ></button>
                  </div>
                  <div class="col-12 d-flex align-items-center">
                    <h1 class="ms-2">{{ keep.name }}</h1>
                    <h5 class="ms-2">
                      <span class="ms-2 mdi mdi-eye"> </span> {{ keep.views }}
                    </h5>
                    <h5>
                      <span class="ms-2 mdi mdi-alpha-k"> </span>
                      {{ keep.kept }}
                    </h5>
                  </div>
                  <div class="col-12">
                    <h5 class="ms-2">{{ keep.description }}</h5>
                  </div>
                  <div class="col-12 d-flex align-items-center">
                    <h6 class="ms-2">Uploaded by:</h6>
                    <h5
                      class="ms-2 text-info darken-20 selectable"
                      @click="profilePage(keep.creator.id)"
                      title="Go to Profile"
                    >
                      {{ keep.creator?.name }}
                    </h5>
                  </div>
                  <div class="col-12 justify-items-end d-flex h-100">
                    <div class="ms-2 d-flex align-items-end">
                      <button
                        v-if="keep.creator?.id == account?.id"
                        type="button"
                        title="delete keep"
                        class="btn btn-primary m-2"
                        @click="deleteKeep(keep.id)"
                      >
                        Delete
                      </button>

                      <div v-if="route.name == 'Vault'">
                        <button
                          type="button"
                          title="unSave from Vault"
                          v-if="keep.vaultKeepId"
                          class="btn btn-info m-2"
                          @click="unSaveKeep(keep.vaultKeepId)"
                        >
                          UnKeep
                        </button>
                      </div>
                      <div class="dropdown">
                        <button
                          v-if="account.id"
                          title="Save to vault"
                          class="btn btn-secondary dropdown-toggle m-2"
                          type="button"
                          id="dropdownMenuButton1"
                          data-bs-toggle="dropdown"
                          aria-expanded="false"
                        >
                          Keep
                        </button>
                        <ul
                          class="dropdown-menu"
                          aria-labelledby="dropdownMenuButton1"
                        >
                          <li v-for="v in vaults" :key="v.id" :vaults="v">
                            <a
                              class="dropdown-item"
                              :title="v.name"
                              @click.prevent="saveToVault(v.name, v.id)"
                              >{{ v.name }}</a
                            >
                          </li>
                        </ul>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState.js"
import { router } from "../router.js"
import { profilesService } from "../services/ProfilesService.js"
import { Modal } from "bootstrap"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { useRoute } from "vue-router"
import { keepsService } from "../services/KeepsService.js"
import { onMounted, watchEffect } from "@vue/runtime-core"
import { vaultsService } from "../services/VaultsService.js"
import { vaultKeepsService } from "../services/VaultKeepsService.js"
export default {
  setup() {
    const route = useRoute()
    return {
      route,
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),
      account: computed(() => AppState.account),
      async profilePage(profileId) {
        try {
          await profilesService.getById(profileId)
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
          router.push({ name: 'Profile', params: { id: profileId } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async deleteKeep(id) {
        if (await Pop.confirm()) {
          try {
            await keepsService.deleteKeep(id)
            Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
          } catch (error) {
            logger.error(error)
            Pop.toast(error.message, 'error')
          }
        }

      },
      async saveToVault(vaultName, vaultId) {
        try {
          const keepId = AppState.activeKeep.id
          await vaultKeepsService.addToVault(vaultId, keepId)
          Pop.toast("Saved to " + vaultName + " vault", "success")
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async unSaveKeep(vaultkeepId) {
        try {
          await keepsService.unSaveKeep(vaultkeepId, route.params.id)
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>