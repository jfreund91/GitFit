<template>
  <div id="app">
   <the-header></the-header>
   <router-view />
   <footer-bar id="footer"></footer-bar>
  </div>
</template>

<script>
import TheHeader from '@/components/layout/Header.vue'
import FooterBar from '@/components/layout/FooterBar.vue'

export default {
  name: 'app',
  components: {
    TheHeader,
    FooterBar
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
