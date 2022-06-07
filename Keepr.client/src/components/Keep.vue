<template>
  <div
    class="keep position-relative bg-dark elevation-2"
    @click="setActive(keep.id)"
  >
    <img class="img-fluid img-keep" :src="keep.img" />
    <div class="bg-shadow">
      <h4 class="keep-title mx-2 text-light">{{ keep.name }}</h4>
    </div>
  </div>
</template>


<script>
import { Modal } from "bootstrap"
import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { keepsService } from "../services/KeepsService.js"
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
      async setActive(id) {
        try {
          await keepsService.getById(id)
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).show()
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
.button-placement {
  position: absolute;
  right: 1em;
  top: 1em;
}
</style>