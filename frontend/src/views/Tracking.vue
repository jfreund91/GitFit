<template>
    <div id="tracking">
        <div class="container circle-container">
            <div id="profile-stats">
                <h3>User Stats</h3>
                <div>Age: {{profile.age}}</div>
                <div>Current Weight: {{profile.currentWeight}}</div>
                <div>Goal Weight: {{profile.goalWeight}}</div>
                <div>Height(in): {{profile.height.feet*12+ +profile.height.inches}}</div>
            </div>
            
            <div class="circle"><h2 class="circle-header">Calories Consumed: {{caloriesConsumed}} </h2></div>
            <div class="circle"><h2 class="circle-header">Calorie Budget: {{calorieBudget - caloriesConsumed}}</h2></div>
        </div>
        <div class="container bars-container">
            <div>
                <h1>Marco Nutrients</h1>
                <h2>Protein</h2>
                <div class="marco" id="protein"></div>
                <h2>Carbs</h2>
                <div class="macro" id="carbs"></div>
                <h2>Fat</h2>
                <div class="macro" id="fat"></div>
            </div>
            <div>
                <h1>Water</h1>
                <div class="glass-container">
                <i @click="addWater()" class="fas fa-plus-circle large-plus"></i>
                <div class="water-glass">
                    <div id="water"></div>
                </div>

                </div>
            </div>
        </div>
        <div>
            <div>
                <div class="meal-list-header">
                    <h2>Snacks</h2>
                </div>
                <div class="container">
                    <i class="fas fa-plus-circle large-plus"></i>
                </div>
            </div>
            <div>                
                <div class="meal-list-header">
                    <h2>Breakfast</h2>
                </div>
                <div class="container">
                    <i class="fas fa-plus-circle large-plus"></i>
                </div>
            </div>
            <div>                
                <div class="meal-list-header">
                    <h2>Lunch</h2>
                </div>
                <div class="container">
                    <i class="fas fa-plus-circle large-plus"></i>
                </div>
            </div>
            <div>                
                <div class="meal-list-header">
                    <h2>Dinner</h2>
                </div>
                <div class="container">
                    <i class="fas fa-plus-circle large-plus"></i>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'tracking',
    data() {
        return {
            water: 0,
            
        }
    },
    methods: {
        addWater() {
            this.water +=1;
            let waterLevel = document.getElementById('water');
            if(this.water === 1) {
                waterLevel.style.height = "12.5%";
            } else if (this.water === 2) {
                waterLevel.style.height = "25%";
            } else if (this.water === 3) {
                waterLevel.style.height = "37.5%";
            } else if (this.water === 4) {
                waterLevel.style.height = "50%";
            } else if (this.water === 5) {
                waterLevel.style.height = "62.5%";
            } else if (this.water === 6) {
                waterLevel.style.height = "75%";
            } else if (this.water === 7) {
                waterLevel.style.height = "87.5%";
            }
            else if (this.water === 8) {
                waterLevel.style.height = "100%";
            }
        }
    },
    computed: {
    calorieBudget () {
        if(this.profile.gender === 'F') { 
            return Math.trunc(655 + (4.35 * this.profile.currentWeight) +
            (4.7 * this.profile.height.feet*12+ +this.profile.height.inches) - (4.7 * this.profile.age))   
        }
        else {
            return Math.trunc(66 + (6.23 * this.profile.currentWeight) +
            (12.7 * this.profile.height.feet*12+ +this.profile.height.inches) - (6.8 * this.profile.age)) 
        }
      
    },
    caloriesConsumed() {
        let sum = 0;
        if(this.profile.eatenToday.length > 0) {
        this.profile.eatenToday.forEach(item => {
            sum += item.kcal;
        })}
        return sum;
    }

    }

}
</script>

<style>

    #water {
        position: absolute;
        left: 50%;
        bottom: 0;
        transform: translateX(-50%);
        width: 210px;
        height: 0%;
        background-color: lightskyblue;
    }

    .water-glass {
        height: 350px;
        width: 250px;
        background-color: whitesmoke;
        position: relative;
    }

    .meal-list-header {
        background-color: #1a174e;
        color: white;
        width: 100%;
        padding: 10px 0px 10px 0px;
        text-align: left;
    }

    #fat {
        height: 30px;
        width: 50%;
        background-color: green;
    }

    #protein {
        height: 30px;
        width: 70%;
        background-color: red;
    }

    #carbs {
        height: 30px;
        width: 90%;
        background-color: blue;
    }

    .meal-list-header h2 {
        margin-left: 100px;
    }

    .bars-container, .glass-container {
        display: flex;
        justify-content: space-between;
    }

    .circle-container {
        display: flex;
        justify-content:space-evenly;
    }


    .large-plus {
        font-size: 50px;
        text-align: left;
    }
    .circle-header {
       position: absolute;
       top: 50%;
       left: 50%;
       margin: 0px;
       transform: translate(-50%, -50%);
    }

    .circle {
        width: 250px;
        height: 250px;
        background-color:royalblue;
        border-radius: 50%;
        position: relative;
        color: white;

    }
    .search-results ul {
        color: black;
        text-align: left;
    }

    i {
        color: green;
        font-size: 30px;
    }

    i:hover {
        cursor: pointer;
    }


    input {
        padding: 10px;
    }

    .container {
        margin: 100px auto;
        width: 80%;
    }

    .search {
        width: 30%;
    }

    .btn {
        width: 10%;
    }

    .search-results li {
        margin: 10px 0px;
        display: flex;
        justify-content: space-between;
    }
 
    .search-results {
        margin-top: 50px;
    }

#tracking {
    padding-bottom: 60px;
}

#profile-stats {
    border: solid 3px black;
    padding: 10px;
    background-color: white;
}
</style>