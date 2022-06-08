<template>
  <nav
    class="
      navbar navbar-expand-lg
      d-flex
      justify-content-between
      navbar-dark
      bg-primary
      px-3
      shadow
    "
  >
    <router-link :to="{ name: 'Home' }">
      <h2 class="text-secondary" title="Return to Home Page">
        <span class="mdi mdi-alpha-k-box"></span>eepr
      </h2>
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
