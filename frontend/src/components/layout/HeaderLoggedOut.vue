<template>
    <header>
        <div class="img-container">
        <router-link to="/"><img src="../../assets/logo.png"></router-link>  
        </div>
        <span id="tagline1">Git fit programming your diet!</span>
        <span id="tagline2">Git your diet under version control.</span>
        <nav>
                <ul id="main-nav">
                    <li>{{userName}}</li>
                     <li><router-link to="/">Home</router-link></li>
                     <li><router-link to="/profile">Profile</router-link></li>
                     <li><router-link to="/tracking">Tracking</router-link></li>
                     <li><router-link to="/search">Search</router-link></li>
                     <li><router-link to="/login">Register/Login</router-link></li>
                </ul>
        </nav>
    </header>
</template>
<script>
    import auth from '@/shared/auth.js'

    export default {
        name: "header-logged-out",
        computed: {
            userName() {
                return auth.getUser();
            }
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

    #tagline1 {
        text-align: center;
        color: rgb(230, 186, 42);
        font-size: 1.3rem;
    }

    #tagline2 {
        text-align: center;
        color: rgb(83, 192, 56);
    }

</style>