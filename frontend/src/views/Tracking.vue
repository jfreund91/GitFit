<template>
    <div id="tracking">
        <h1 id="tracking-header">Track Calories</h1>
        <div class="container circle-container">
            <div id="profile-stats">
                <h3>User Stats</h3>
                <div>Age: {{profile.age}}</div>
                <div>Current Weight: {{profile.currentWeight}}</div>
                <div>Goal Weight: {{profile.goalWeight}}</div>
                <div>Height(in): {{profile.height.feet*12+ +profile.height.inches}}</div>
                <button id="removeLastEntry" @click="removeLastEntry()">Remove last entry</button>
            </div>
            <div class="circle"><h2 class="circle-header">Calories Consumed: {{Math.trunc(caloriesConsumed)}} </h2></div>
            <div class="circle"><h2 class="circle-header">Calorie Budget: {{Math.trunc(calorieBudget - caloriesConsumed)}}</h2></div>
        </div>
        <div class="container bars-container">
            <div>
                <h1>Macro Nutrients</h1>
                <h2>Protein</h2>
                <div class="macro" id="protein"><strong>{{Math.trunc(this.proteinConsumed)}} g</strong></div>
                <h2>Carbs</h2>
                <div class="macro" id="carbs"><strong>{{Math.trunc(this.carbsConsumed)}} g</strong></div>
                <h2>Fat</h2>
                <div class="macro" id="fat"><strong>{{Math.trunc(this.fatConsumed)}} g</strong></div>
            </div>
            <div>
                <h1>Water</h1>
                <div class="glass-container">
                    <i @click="addWater()" class="fas fa-plus-circle large-plus"></i>
                <div class="water-glass">
                    <h1>{{this.profile.water}} glasses</h1>
                    <div id="water">
                    </div>
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
                    <router-link to="/search"><i class="fas fa-plus-circle large-plus"></i></router-link>
                    <ul>
                        <li v-for="item in snacks" :key="item.id">
                            <span class="remove-food">
                                <i class="fas fa-minus-circle large-minus" @click="removeFood(item.id)"></i>
                            </span>{{item.name}}
                        </li>
                    </ul>
                </div>
            </div>
            <div>                
                <div class="meal-list-header">
                    <h2>Breakfast</h2>
                </div>
                <div class="container">
                    <router-link to="/search"><i class="fas fa-plus-circle large-plus"></i></router-link>
                    <ul>
                        <li v-for="item in breakfasts" :key="item.id">
                            <span class="remove-food">
                                <i class="fas fa-minus-circle large-minus" @click="removeFood(item.id)"></i>
                            </span>{{item.name}}
                        </li>
                    </ul>
                </div>
            </div>
            <div>                
                <div class="meal-list-header">
                    <h2>Lunch</h2>
                </div>
                <div class="container">
                    <router-link to="/search"><i class="fas fa-plus-circle large-plus"></i></router-link>
                    <ul>
                        <li class = "meal-items" v-for="item in lunches" :key="item.id">
                            <span class="remove-food">
                                <i class="fas fa-minus-circle large-minus" @click="removeFood(item.id)"></i>
                            </span>{{item.name}}
                        </li>
                    </ul>
                </div>
            </div>
            <div>
                <div class="meal-list-header">
                    <h2>Dinner</h2>
                </div>
                <div class="container">
                    <router-link to="/search"><i class="fas fa-plus-circle large-plus"></i></router-link>
                    <ul>
                        <li v-for="item in dinners" :key="item.id">
                            <span class="remove-food">
                                <i class="fas fa-minus-circle large-minus" @click="removeFood(item.id)"></i>
                                </span>{{item.name}}
                        </li>
                    </ul>
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
            
            }
        },
    methods: {
        addWater() {
            this.profile.water +=1;
            let waterLevel = document.getElementById('water');
            if(this.profile.water === 1) {
                waterLevel.style.height = "12.5%";
            } else if (this.profile.water === 2) {
                waterLevel.style.height = "25%";
            } else if (this.profile.water === 3) {
                waterLevel.style.height = "37.5%";
            } else if (this.profile.water === 4) {
                waterLevel.style.height = "50%";
            } else if (this.profile.water === 5) {
                waterLevel.style.height = "62.5%";
            } else if (this.profile.water === 6) {
                waterLevel.style.height = "75%";
            } else if (this.profile.water === 7) {
                waterLevel.style.height = "87.5%";
            }
            else if (this.profile.water >= 8) {
                waterLevel.style.height = "100%";
            }
        },
        removeLastEntry(){
        this.profile.eatenToday.pop();
        },
        removeFood(foodId) {
            let output = [];
            output = this.profile.eatenToday.filter((item)=> {
                return item.id !== foodId;
            });
            this.profile.eatenToday = output;
        }
    },
    computed: {
    calorieBudget () {
        if(this.profile.gender === 'F') { 
            return Math.trunc(655 + (4.35 * this.profile.currentWeight) +
            (4.7 * this.profile.height.feet*12+ +this.profile.height.inches) - (4.7 * this.profile.age)
            )* this.profile.activityLevel + (this.profile.timeline * 1)
            
        }
        else {
            return Math.trunc(66 + (6.23 * this.profile.currentWeight) +
            (12.7 * this.profile.height.feet*12+ +this.profile.height.inches) - (6.8 * this.profile.age)
            )* this.profile.activityLevel + (this.profile.timeline * 1)
        }
      
    },
    caloriesConsumed() {
        let sum = 0;
        if(this.profile.eatenToday.length > 0) {
        this.profile.eatenToday.forEach(item => {
            sum += item.calories;
        })}
        return sum;
    },
    fatConsumed() {
        let totalFat = 0;
        if(this.profile.eatenToday.length > 0) {
            this.profile.eatenToday.forEach(item => {
                totalFat += item.fat;
            })
        }
        return totalFat;
    },
    carbsConsumed() {
        let totalCarbs = 0;
        if(this.profile.eatenToday.length > 0) {
            this.profile.eatenToday.forEach(item => {
                totalCarbs += item.carbs;
            })
        }
        return totalCarbs;
    },
    proteinConsumed() {
        let totalProtein = 0;
        if(this.profile.eatenToday.length > 0) {
            this.profile.eatenToday.forEach(item => {
                totalProtein += item.protein;
            })
        }
        return totalProtein;
    },
    snacks() {
        let snacks = [];
        snacks = this.profile.eatenToday.filter((snack) => {
            return snack.mealType === 'Snack'
        });
        return snacks;
    },
    breakfasts () {
        let breakfasts = [];
        breakfasts = this.profile.eatenToday.filter((item) => {
            return item.mealType === 'Breakfast'
        });
        return breakfasts;
    },
    lunches () {
        let lunches = [];
        lunches = this.profile.eatenToday.filter((item) => {
            return item.mealType === 'Lunch'
        });
        return lunches;
    },
    dinners() {
        let dinners = [];
        dinners = this.profile.eatenToday.filter((item)=> {
            return item.mealType === 'Dinner'
        });
        return dinners;
    }
    }


}

</script>

<style>

    #water {
        position: absolute;
        left: 50%;
        bottom: 0;
        width: 220px;
        height: 0%;
        background-color: lightskyblue;
        transform: translate(-50%);
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

    .water-glass h1 {
        position: relative;
        top: 50%;
        color: black;
        left: 50%;
        transform: translate(-50%, -50%);
        text-align: center;
        margin: 0;
        z-index: 3;

    }

    .bars-container, .glass-container {
        display: flex;
        justify-content: space-between;
    }

    .circle-container {
        display: flex;
        justify-content:space-evenly;
    }

    .remove-food {
        padding-right: 50px;
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
    .large-minus {
        color: red;
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
    border: solid .5px black;
    padding: 10px;
    background-color: white;
}

#tracking-header {
    text-align: center;
}
.macro {
    color: white;
}
li {
    list-style: none;
    padding-bottom: 25px;
}
</style>