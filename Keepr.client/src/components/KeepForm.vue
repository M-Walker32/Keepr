<template>
  <form @submit.prevent="createKeep">
    <!-- name -->
    <div class="form-group">
      <label for="Name">Name</label>
      <input
        type="text"
        class="form-control"
        id="name"
        required
        max="50"
        placeholder="Cheesecake Recipe"
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
        required
        max="100"
        placeholder="description..."
        v-model="editable.description"
      />
    </div>
    <!-- img -->
    <div class="form-group">
      <label for="Image">Image</label>
      <input
        type="text"
        class="form-control"
        id="img"
        required
        max="250"
        placeholder="https://thiscatdoesnotexist.com"
        v-model="editable.img"
      />
    </div>
    <button type="submit" class="btn btn-secondary mt-2" title="Create Keep">
      Create
    </button>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import { keepsService } from "../services/KeepsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { Modal } from "bootstrap"
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
          Modal.getOrCreateInstance(document.getElementById('create-keep-form')).hide()
          editable.value = ''
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
          editable.value = ''
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>