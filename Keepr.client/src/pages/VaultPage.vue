<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 mt-2 p-5">
        <div class="d-flex">
          <h1>
            {{ vault.name }}<i v-if="vault.isPrivate" class="mdi mdi-lock"> </i>
          </h1>
        </div>
        <div class="d-flex">
          <button
            class="btn btn-outline-primary mx-2"
            title="delete vault"
            @click="deleteVault(vault.id)"
          >
            Delete
          </button>
          <h5>
            {{ vault.description }} <br />
            Keeps: {{ keeps.length }}
          </h5>
        </div>

        <!-- <div>{{ keeps[0] }}</div> -->
      </div>
    </div>
  </div>
  <div class="masonary">
    <Keep v-for="k in keeps" :key="k.id" :keep="k" />
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState.js"
import { onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { keepsService } from "../services/KeepsService.js"
import { vaultsService } from "../services/VaultsService.js"
import { useRoute, useRouter } from "vue-router"
export default {
  setup() {
    const router = useRouter()
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getById(route.params.id)
        await keepsService.getVaultKeeps(route.params.id)
      } catch (error) {
        Pop.error("Invalid Entry")
        router.push({ name: 'Home' })
      }
    })
    return {
      async deleteVault(id) {
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(id)
            Pop.toast("Vault Deleted", "success")
            router.push({ name: 'Home' })
          }
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message, 'error')
        }
      },
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps),
    }
  }
}
</script>


<style lang="scss" scoped>
</style>