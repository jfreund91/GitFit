<template>
    <div id="landing-page">
    <div id="landing">
        <div id="landing-menu">
            <h1>Welcome</h1>
        </div>
    </div>
    <div id="box-container" class="container">
        <div class="box">
            <h2>Healthy Choices</h2>
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
    <div class="table-container">
        <p id="welcome-message">Welcome to the TE GitFit tracker! There are some benefits to becoming a registered user highlighted in 
            the table below.
        </p>


    <table class="landing-table">
        <tr>
            <th>Feature</th>
            <th>Guest</th>
            <th>Account</th>
        </tr>
        <tr>
            <td>Track Daily Calories</td>
            <td><i id="check" class="fas fa-check"></i></td>
            <td><i id="check" class="fas fa-check"></i></td>
        </tr>
        <tr>
            <td>Long Term Tracking</td>
            <td><i id="x" class="fas fa-times"></i></td>
            <td><i id="check" class="fas fa-check"></i></td>
        </tr>
                <tr>
            <td>Quick Meals</td>
            <td><i id="x" class="fas fa-times"></i></td>
            <td><i id="check" class="fas fa-check"></i></td>
        </tr>
                <tr>
            <td>Favorites</td>
            <td><i id="x" class="fas fa-times"></i></td>
            <td><i id="check" class="fas fa-check"></i></td>
        </tr>
    </table>
    <div id="btn-container">
    <router-link to="/profile"><button id="landing-btn">Guest</button></router-link>
    <router-link to="/login"><button id="landing-btn">Sign Up</button></router-link>
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
    border: 1px solid black;
}

#btn-container {
    width: 80%;
    margin: 0 auto;
    text-align: center;
}

.landing-table th {
    text-align: left;
}

#landing-page {
padding-bottom: 60px;
}

#landing-btn {
    width: 25%;
    margin: 10px;
}

.table-container {
    width: 80%;
    margin: 50px auto;
}

i#x.fas.fa-times {
    color: red;
}

i#x.fas.fa-times:hover {
    cursor: default;
}

i#check.fas.fa-check:hover {
    cursor: default;
}

.landing-table {
    width: 100%;
}


#box-container {
    display: flex;
    justify-content: space-between;
    flex-wrap: wrap;
}

#landing-menu h1 {
    color: white;
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
    /* color: #dc5001; */
    color: #61c02a;
    font-weight: 700;
}

#landing-menu h1 {
    font-size: 75px;
    margin: 0px;
    padding-top: 40px;
}


#landing {
  background-image: url("../assets/food.jpg");
  text-align: center;
  height: 250px;
  background-size: cover;
}

#landing-menu {
    padding: 0px;
}

.landing-opts {
    background-color: gray;
    color: white;
}

@media screen and (max-width: 789px) {
    #box-container {
        display: block;
    }
    
    .box {
        width: 100%;
    }
}

.landing-table {
    border: black 4px solid;
    margin-bottom: 50px;
}

.landing-table th {
    /* border: solid pink 1px; */
    background-color: yellowgreen;
}

.landing-table td {
    border: solid purple 1px;
}

#welcome-message {
    font-weight: 700;
    border-top: 2px black dotted;
    padding-top: 10px;

}

</style>
