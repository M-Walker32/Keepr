<template>
  <div class="masonary">
    <!-- KEEPS HERE -->
    <Keep v-for="k in keep" :key="k.id" :keep="k" />
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
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getKeeps()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keep: computed(() => AppState.keeps)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>

<style scoped lang="scss">
@import "../assets/scss/main.scss";
</style>
