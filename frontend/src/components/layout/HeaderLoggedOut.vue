<template>
    <header>
        <div class="img-container">
        <img src="../../assets/logo.png">
        </div>
        <nav>
                <ul id="main-nav">
                    <li><router-link to="/login">Register/Login</router-link></li>
                    <li><router-link to="/profile">Home</router-link></li>
                </ul>
        </nav>
    </header>
</template>
<script>
    export default {
        name: "header-logged-out"
    }
</script>
<style >
    header {
        display: flex;
        width: 100%;
        background-color: #1a174e;
        justify-content: space-between;
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

    img { 
        width: 100%;
        height: auto;
    }

    .img-container {
        width: 25%;
    }

</style>