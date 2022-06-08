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
        required
        max="50"
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
        required
        max="250"
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
    <button type="submit" class="btn btn-secondary mt-2" title="Create Keep">
      Create
    </button>
  </form>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { keepsService } from "../services/KeepsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { vaultsService } from "../services/VaultsService.js"
import { AppState } from "../AppState.js"
import { Modal } from "bootstrap"
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createVault() {
        try {
          editable.CreatorId = this.account.id
          await vaultsService.createVault(editable.value)
          Modal.getOrCreateInstance(document.getElementById('create-vault-form')).hide()
          editable.value = ''
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
          editable.value = ''
        }
      },
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>