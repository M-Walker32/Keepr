<template>
  <div
    class="keep position-relative bg-dark elevation-2"
    title="expand keep"
    @click="setActive(keep.id)"
  >
    <img class="img-fluid img-keep" :src="keep.img" :alt="keep.name" />
    <div class="bg-shadow d-flex d-flex align-items-end">
      <img
        :src="keep.creator.picture"
        class="profile-keep-img m-2 hover"
        :alt="keep.creator.name"
      />
      <h4 class="p-1 keep-title text-light hover">
        {{ keep.name }}
      </h4>
    </div>
  </div>
</template>


<script>
import { Modal } from "bootstrap"
import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { keepsService } from "../services/KeepsService.js"
import { useRoute } from "vue-router"
import { computed } from "@vue/reactivity"
import { onMounted } from "@vue/runtime-core"
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    const route = useRoute()
    return {
      activekeep: computed(() => AppState.activeKeep),
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