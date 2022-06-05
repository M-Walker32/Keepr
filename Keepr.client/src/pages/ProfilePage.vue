<template>
  <div class="container">
    <div class="row mt-5">
      <div class="col-2">
        <img
          class="img-fluid rounded-circle height ms-1"
          :src="profile.picture"
        />
      </div>
      <div class="col-2">
        <h5 class="ms-2">
          {{ profile.name }}
        </h5>
        <h5 class="ms-2">Vaults: {{ vaults.length }}</h5>
        <h5 class="ms-2">Keeps: {{ keeps.length }}</h5>
      </div>
    </div>
  </div>
  <hr />
  <h2 class="ms-4">Vaults <span class="mdi mdi-plus selectable"></span></h2>
  <!-- VAULTS HERE -->
  <div class="masonary">
    <Vault v-for="v in vaults" :key="v.id" :vault="v" />
  </div>
  <hr />
  <h2 class="ms-4">Keeps<span class="mdi mdi-plus selectable"></span></h2>
  <!-- KEEPS HERE -->
  <div class="masonary">
    <Keep v-for="k in keeps" :key="k.id" :keep="k" />
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState.js"
import { profilesService } from "../services/ProfilesService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { useRoute } from "vue-router";
import { onMounted } from "@vue/runtime-core";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getById(route.params.id);
        await keepsService.getProfileKeeps(route.params.id)
        await vaultsService.getProfileVaults(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.myKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>