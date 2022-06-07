<template>
  <form @submit.prevent="createKeep">
    <!-- name -->
    <div class="form-group">
      <label for="Name">Name</label>
      <input
        type="text"
        class="form-control"
        id="name"
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
        placeholder="https://thiscatdoesnotexist.com"
        v-model="editable.img"
      />
    </div>
    <button type="submit">Create</button>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import { keepsService } from "../services/KeepsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
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