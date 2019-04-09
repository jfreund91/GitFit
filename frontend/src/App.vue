<template>
  <div id="app">
   <!-- <HelloWorld msg="Welcome to Your Vue.js App"/> -->
   <header-logged-out></header-logged-out>
   <profile />
  </div>
</template>

<script>
import HeaderLoggedIn from './components/layout/HeaderLoggedIn.vue'
import HeaderLoggedOut from './components/layout/HeaderLoggedOut.vue'
import Profile from './components/views/Profile.vue'

export default {
  name: 'app',
  components: {
    HeaderLoggedIn,
    HeaderLoggedOut,
    Profile
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
}

body {
  background-color:floralwhite;
}
</style>
