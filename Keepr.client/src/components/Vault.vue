<template>
  <div
    class="keep position-relative bg-dark elevation-2"
    @click="vaultPage(vault.id)"
  >
    <img class="img-fluid img-keep" src="https://thiscatdoesnotexist.com" />
    <div class="bg-shadow">
      <h4 class="keep-title mx-2 text-light">{{ vault.name }}</h4>
    </div>
  </div>
</template>


<script>
import { Modal } from "bootstrap"
import { vaultsService } from "../services/VaultsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { useRouter } from "vue-router"
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    const router = useRouter()
    return {
      async vaultPage(vaultId) {
        try {
          await vaultsService.getById(vaultId)
          router.push({ name: 'Vault', params: { id: vaultId } })
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