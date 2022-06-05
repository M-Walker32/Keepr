<template>
  <nav
    class="
      navbar navbar-expand-lg
      d-flex
      justify-content-between
      navbar-dark
      bg-primary
      px-3
    "
  >
    <router-link :to="{ name: 'Home' }">
      <h2 class="text-dark">Keepr</h2>
    </router-link>
    <div><Login /></div>
  </nav>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
