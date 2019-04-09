<template>
  <div id="app">
   <!-- <HelloWorld msg="Welcome to Your Vue.js App"/> -->
   <header-logged-out></header-logged-out>
   <profile></profile>
   <tracking></tracking>
  </div>
</template>

<script>
import HeaderLoggedOut from '@/components/layout/HeaderLoggedOut.vue'
import HeaderLoggedIn from '@/components/layout/HeaderLoggedIn.vue'
import Profile from '@/components/views/Profile.vue'
//import HelloWorld from './components/HelloWorld.vue'
import Tracking from './components/views/Tracking.vue'

export default {
  name: 'app',
  components: {
    HeaderLoggedOut,
    HeaderLoggedIn,
    Profile,  
    Tracking
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

body {
  margin: 0;
  background-color:#ececeb;
}
</style>
