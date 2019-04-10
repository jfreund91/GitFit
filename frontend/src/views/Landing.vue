<template>
    <div>
    <div id="landing">
        <div id="landing-menu">
            <h1>Welcome</h1>
        </div>
    </div>
    <h2 class="links"><router-link to="/login">Login</router-link></h2>
    <h2 class="links"><router-link to="/profile">Continue as Guest</router-link></h2>
    </div>
</template>
<script>
</script>
<style>
@import url('https://fonts.googleapis.com/css?family=Fascinate+Inline');

#landing {
    text-align: center;
}
#landing-menu h2 a {
    color: white;
    text-decoration: none;
    /* text-shadow: 2px 2px #6856b9; */
    font-size: 1.5rem;
}

.links a:hover {
    text-transform: uppercase;
    color: #e1520e;
}

#landing-menu h2 {
    font-size: 50px;
    margin: 0px;
}

.links {
    text-align: center;
    font-family: 'Fascinate Inline', cursive;
    font-size: 30px;
}

.links a {
    text-decoration: none;
    color: #1a174e;
}

#landing-menu h1 {
    font-size: 75px;
    margin: 0px;
}

#landing-menu {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
}

#landing {
  background-image: url("../assets/food.jpg") ;
  text-align: center;
  height: 250px;
  background-size: cover;
}

#landing-menu {
    padding: 0px;
}
</style>
