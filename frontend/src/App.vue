<template>
  <div id="app">
   <header-logged-out></header-logged-out>
   <router-view />
   <footer-bar id="footer"></footer-bar>
  </div>
</template>

<script>
import HeaderLoggedOut from '@/components/layout/HeaderLoggedOut.vue'
import FooterBar from '@/components/layout/FooterBar.vue'

export default {
  name: 'app',
  components: {
    HeaderLoggedOut,
    FooterBar,
  }
}
</script>

<style>
#app {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  display: flex;
  flex-direction: column;
  /* justify-content: space-between; */

  min-height:100%;
  position:relative;
}



body, html {
  margin: 0;
  height: 100%;
  padding: 0;
  background-color:#ececeb;
}




</style>
