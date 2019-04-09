<template>
  <div id="app">
   <header-logged-out></header-logged-out>
   <router-view />
   <footer-bar></footer-bar>
  </div>
</template>

<script>
import HeaderLoggedOut from '@/components/layout/HeaderLoggedOut.vue'
// import HeaderLoggedIn from '@/components/layout/HeaderLoggedIn.vue'
import FooterBar from '@/components/layout/FooterBar.vue'

export default {
  name: 'app',
  components: {
    HeaderLoggedOut,
    FooterBar
    // HeaderLoggedIn,
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

body {
  margin: 0;
  background-color:#ececeb;
}

</style>
