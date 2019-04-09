<template>
  <div id="app">
   <!-- <HelloWorld msg="Welcome to Your Vue.js App"/> -->
   <header-logged-out></header-logged-out>
   <router-view />
   <footer-bar id="footer-section"></footer-bar>
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
  text-align: center;
}

body {
  margin: 0;
  background-color:#ececeb;
}

#footer-section {
  position: absolute;
  bottom: 0;
  width: 100%;
}

</style>
