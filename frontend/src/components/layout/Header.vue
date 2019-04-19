<template>
    <header>
        <div class="img-container">
        <router-link to="/"><img src="../../assets/logo.png"></router-link>  
        </div>
        <span id="tagline2"></span>
        <nav>
                <ul id="main-nav">
                    <li id="user-name-header">Welcome {{userName}}!</li>
                     <li><router-link to="/">Home</router-link></li>
                     <li><router-link to="/profile">Profile</router-link></li>
                     <!-- Don't show these if user hasn't filled out profile yet. -->        
                        <li v-if="isValidProfile"><router-link to="/tracking">Tracking</router-link></li>
                        <li v-if="isValidProfile"><router-link to="/search">Search</router-link></li>
                    <li v-if="(isAuthenticated)">
                        <a href="/login" v-on:click="logout">Logout</a>
                    </li>
                    <li v-if="!(isAuthenticated)">
                        <router-link to="/login">Login</router-link>
                    </li>
                </ul>
        </nav>
    </header>
</template>
<script>
    import auth from '@/shared/auth.js'
    import prof from '@/shared/prof.js'

    export default {
        name: "the-header",
          data() {
            return {
                 isAuthenticated: auth.getUser() !== null,
                 isValidProfile: prof.isValidProfile() === true, // If profile already exists, this will be true
                 userName: this.getUserName()
    };
  },
  methods: {
    /**
     * Logs the user out and takes them to the login page
     * @function
     */
    logout() {
      auth.destroyToken();
    //   prof.destroyProfile();
      this.$emit('logout'); // Added to remove login details from header
      this.$router.push("/login");
      //prof.destroyProfile(); // Remove profile from local storage upon logout
    },
    getUserName() {
        if(auth.getUser() != null){
        return auth.getUser().sub;
        }
        return auth.getUser();

    },
  }, 
  watch: {
    '$route' (to, from) {
      // react to route changes...
      this.isAuthenticated = auth.getUser() !== null
      this.isValidProfile = prof.isValidProfile() === true
      this.userName = this.getUserName()
    }
  },
  computed: {
    getUser() {
      return auth.getUser();
    },
    
  }

}
</script>
<style >
    header {
        display: flex;
        width: 100%;
        background-color: #1a174e;
        justify-content: space-between;
        align-items:center;
    }

    nav {
        align-self: center;
        display: flex;
        padding: 20px;
    }

    #main-nav {
        padding-left: 0;
        list-style: none;
        color: white;
        display: flex;
    }

    #main-nav a {
        color: white;
        text-decoration: none;
        margin-left: 30px;
    }

    #main-nav a:hover {
        color: #dc5001;
    }

    img { 
        width: 100%;
        height: auto;
    }

    .img-container {
        width: 25%;
    }

    #tagline2 {
        text-align: center;
        color: rgb(230, 186, 42);
        font-size: 1.1rem;
    }

    #tagline1 {
        text-align: center;
        color: rgb(83, 192, 56);
    }

    #profile h1 {
        margin-bottom: 50px;
    }

#tagline2 img {
    width: 200px;
}

#main-nav .router-link-exact-active {
  color: #dc5001;
}

#user-name-header {
    color: #84a617;
}


</style>