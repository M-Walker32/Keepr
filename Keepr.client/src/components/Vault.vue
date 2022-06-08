<template>
  <div
    class="keep position-relative bg-secondary elevation-2"
    title="open vault"
    @click="vaultPage(vault.id)"
  >
    <!-- <div class="bg-secondary vault"></div> -->
    <img
      class="vault img-keep"
      src="https://images.unsplash.com/photo-1454117096348-e4abbeba002c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8Z2VvbWV0cnl8ZW58MHx8MHx8&auto=format&fit=crop&w=500&q=60"
      :alt="vault.name"
    />
    <div class="bg-shadow">
      <h4 class="keep-title p-2 text-light d-flex h-100 align-items-end">
        <span v-if="vault.isPrivate" class="mdi mdi-lock"> </span
        >{{ vault.name }}
      </h4>
    </div>
  </div>
</template>


<script>
import { Modal } from "bootstrap"
import { vaultsService } from "../services/VaultsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { useRouter } from "vue-router"
import { computed, onMounted, ref } from "@vue/runtime-core"
import { AppState } from "../AppState.js"
import { keepsService } from "../services/KeepsService.js"
export default {
  // TODO make this ref refrence the index of the keep that matches the vault
  // num = ref[3],
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    // onMounted(async () => {
    //   try {
    //     await keepsService.getVaultKeeps(props.vault.id)
    //   } catch (error) {
    //     logger.error(error)
    //     Pop.toast(error.message, 'error')
    //   }
    // })
    return {
      async vaultPage(vaultId) {
        try {
          await vaultsService.getById(vaultId)
          router.push({ name: 'Vault', params: { id: vaultId } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      vaultkeep: computed(() => AppState.vaultKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.vault {
  max-width: 100%;
  max-height: 100%;
}
</style>