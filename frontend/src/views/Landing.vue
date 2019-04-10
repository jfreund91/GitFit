<template>
    <div id="landing">
        <h1>Welcome</h1>
        <div id="landing-menu">
            <h2><router-link to="/login">Login</router-link></h2>
            <h2><router-link to="/profile">Continue as Guest</router-link></h2>
        </div>
    </div>
</template>
<script>
</script>
<style>
#landing {
    text-align: center;
}
#landing-menu h2 a {
    color: white;
    text-decoration: none;
    /* text-shadow: 2px 2px #6856b9; */
    font-size: 1.5rem;
}

#landing-menu h2 a:hover {
    text-transform: uppercase;
    color: orange;
}

#landing {
  background-image: url("../assets/food.jpg") ;
  text-align: center;
  height: 200px;
}

#landing-menu {
    padding: 0px;
}
</style>
