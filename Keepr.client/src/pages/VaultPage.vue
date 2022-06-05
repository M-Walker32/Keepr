<template>
  <div class="container">
    <div class="row">
      <div class="col-12">
        <div class="d-flex">
          <h1>{{ vault.name }}</h1>
          <button class="btn btn-outline-primary">Delete</button>
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
export default {
  setup() {
    let vault = AppState.activeVault
    onMounted(async () => {
      try {
        await keepsService.getVaultKeeps(vault.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps),
    }
  }
}
</script>


<style lang="scss" scoped>
</style>