<template>
  <form @submit.prevent="createVault">
    <!-- name -->
    <div class="form-group">
      <label for="Name">Name</label>
      <input
        type="text"
        class="form-control"
        id="name"
        placeholder="Cheesecakes"
        v-model="editable.name"
      />
    </div>
    <!-- description -->
    <div class="form-group">
      <label for="Description">Description</label>
      <input
        type="text"
        class="form-control"
        id="description"
        placeholder="description..."
        v-model="editable.description"
      />
    </div>
    <div>
      <input
        type="checkbox"
        placeholder="Private"
        v-model="editable.IsPrivate"
      />
      <label for="isPrivate" class="ms-1">Private Vault</label>
    </div>
    <button type="submit">Create</button>
  </form>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { keepsService } from "../services/KeepsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { vaultsService } from "../services/VaultsService.js"
import { AppState } from "../AppState.js"
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createVault() {
        try {
          editable.CreatorId = this.account.id
          await vaultsService.createVault(editable.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>