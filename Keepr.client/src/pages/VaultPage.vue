<template>
  <div class="container">
    <div class="row">
      <div class="col-12">
        <div class="d-flex">
          <h1>{{ vault.name }}</h1>
          <button
            class="btn btn-outline-danger ms-1"
            title="delete vault"
            @click="deleteVault(vault.id)"
          >
            Delete
          </button>
        </div>
        <h6>{{ vault.description }}</h6>
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
    let vault = AppState.activeVault
    const router = useRouter()
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getById(route.params.id)
        // await keepsService.getVaultKeeps(route.params.id)
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