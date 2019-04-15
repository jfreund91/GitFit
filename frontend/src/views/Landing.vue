<template>
    <div>
    <div id="landing">
        <div id="landing-menu">
            <h1>Welcome</h1>
        </div>
    </div>
    <h2 class="links"><router-link to="/login">Login</router-link></h2>
    <h2 class="links"><router-link to="/profile">Continue as Guest</router-link></h2>
    <div id="box-container" class="container">
        <div class="box">
            <h2>Healthy Choies</h2>
            <img src="../assets/healthyeating.gif">
            <p>Keep track of your calorie intake based on your nurition goals! 
                You can see the nutritional breakdown in your favorite foods to make healthier choices.</p>
        </div>
        <div class="box">
            <h2>Git Moving</h2>
            <img src="../assets/workingout.gif">
            <p>A healthy lifestyle is a combonination of diet and exercise. Track your calorie intake along with calories burned during exercise.</p>
        </div>
        <div class="box">
            <h2>Track Progress</h2>
            <img src="../assets/measure.gif">
            <p>View your daily, weekly, monthly, and annual progress on your fitness journey! </p>
        </div>
    </div>
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
    text-shadow: 2px 2px #6856b9;
    font-size: 1.5rem;
}

#box-container {
    display: flex;
    justify-content: space-between;
    margin-bottom: 150px;
}

.box {
    width: 30%;
}


.box h2 {
    text-align: center;
}

.links a:hover {
    text-transform: uppercase;
    /* color: #e1520e; */
    letter-spacing: 1px;
    color: #232d7a;
}

#landing-menu h2 {
    font-size: 75px;
    margin: 0px;
}

.links {
    text-align: center;
    /* font-family: 'Fascinate Inline', cursive; */
    font-family: 'Montserrat', sans-serif;
    font-size: 30px;
}

.links a {
    text-decoration: none;
    /* color: #1a174e; */
    color: #dc5001;
    font-weight: 700;
}

#landing-menu h1 {
    font-size: 75px;
    margin: 0px;
    padding-top: 40px;
}

/* #landing-menu {
    position; 
    top: 50%;
    left: 50%; */
    /* transform: translate(-50%, -50%); */
/* }  */

#landing {
  background-image: url("../assets/food.jpg");
  text-align: center;
  height: 250px;
  background-size: cover;
}

#landing-menu {
    padding: 0px;
}


</style>
