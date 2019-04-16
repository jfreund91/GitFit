<template>
    <div id="tracking">
        <h1 id="tracking-header">Track Calories</h1>
        <div class="container circle-container">
            <div id="profile-stats">
                <h3>User Stats</h3>
                <!-- <div>Age: {{profile.age}}</div> -->
                <div>Age: {{calculateAge}}</div>
                <div>Current Weight: {{profile.currentWeight}}</div>
                <div>Goal Weight: {{profile.goalWeight}}</div>
                <div>Height(in): {{profile.feet*12+ +profile.inches}}</div>
                <button id="removeLastEntry" @click="removeLastEntry()">Remove last entry</button>
            </div>
            <column-chart :data="[['Calories Consumed', Math.trunc(caloriesConsumed)], ['Calorie Budget', Math.trunc(calorieBudget - caloriesConsumed) ]]" backgroundColor=""></column-chart>
        </div>
        <div class="container bars-container">
            <div class="macros">
                <h1>% Based On FDA Recommended Value</h1>
            <bar-chart suffix="%" :data="[['Protein', Math.trunc((this.proteinConsumed/50)*100)], ['Carbs', Math.trunc((this.carbsConsumed/300)*100)], ['Fat', Math.trunc((this.fatConsumed/80)*100)]]"></bar-chart>
            <p>Protein: {{Math.trunc(this.proteinConsumed)}} grams</p>
            <p>Carbs: {{Math.trunc(this.carbsConsumed)}} grams</p>
            <p>Fat: {{Math.trunc(this.fatConsumed)}} grams</p>
            <p>Calories: {{Math.trunc(this.caloriesConsumed)}}</p>
            </div>
            <div class="water-container">
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
                            {{item.name}}
                            <span class="edit-food"><i class="far fa-edit"></i></span>
                            <span class="remove-food">
                                <i class="fas fa-minus-circle large-minus" @click="removeFood(item.id)"></i>
                            </span>
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
                            {{item.name}}
                            <span class="edit-food"><i class="far fa-edit"></i></span>
                            <span class="remove-food">
                                <i class="fas fa-minus-circle large-minus" @click="removeFood(item.id)"></i>
                            </span>
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
                            {{item.name}}
                            <span class="edit-food"><i class="far fa-edit"></i></span>
                            <span class="remove-food">
                                <i class="fas fa-minus-circle large-minus" @click="removeFood(item)"></i>
                            </span>
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
                            {{item.name}}
                            <span class="edit-food"><i class="far fa-edit"></i></span>
                            <span class="remove-food">
                                <i class="fas fa-minus-circle large-minus" @click="removeFood(item.id)"></i>
                            </span>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import Vue from 'vue'
import VueChartkick from 'vue-chartkick'
import Chart from 'chart.js'
import auth from '@/shared/auth.js' // import whether user is logged in

Vue.use(VueChartkick, {adapter: Chart})
export default {
    name: 'tracking',
    data() {
        return {
            
            }
        },
    created() {
        let userInCreated = auth.getUser();
        if(userInCreated !== null) {
            // Call the API to get the user's profile
            //https://localhost:44392/api/profile
            fetch(`${process.env.VUE_APP_REMOTE_API}/tracking`, {
            method: 'GET',
            headers: {
                    "Authorization": 'Bearer ' + auth.getToken() 
                    }, 
            })
            .then((response) => {
            return response.json();
            }).then ((json) => {
                this.profile.eatenToday = [];
                json.forEach(item => {
                this.profile.eatenToday.push({
                    
                    id: item.entryId,
                    name: item.name,
                    calories: item.calories,
                    fat: item.fat,
                    carbs: item.carbs, 
                    protein: item.protein,
                    mealType: item.mealType,
                    servings: item.servings
                })})
            });
        }
    },
    methods: {
         show () {
            this.$modal.show('food-item-detail-view');
        },
        hide () {
            this.$modal.hide('food-item-detail-view');
        },
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
            let user = auth.getUser();
            if (user == null) {
            let output = [];
            output = this.profile.eatenToday.filter((item)=> {
                return item.id !== foodId;
            });
            this.profile.eatenToday = output;
            } else if (user != null) {
                let output = [];
                output = this.profile.eatenToday.filter((item)=> {
                    return item.id === foodId;
                });
                fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/remove`, {
                method: "DELETE",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                },
                body: JSON.stringify(output[0])
                })
            }
        }
    },
    computed: {
        calculateAge() {
        let DateOfBirth = new Date(this.profile.birthDate);
        let currentDate = new Date();
        let dateDiff = currentDate-DateOfBirth; // This is the difference in milliseconds
        let age = Math.floor(dateDiff/31557600000); // Divide by 1000*60*60*24*365.25

        return age;
    },
    calorieBudget () {
        if(this.profile.gender === 'F') { 
            return Math.trunc(655 + (4.35 * this.profile.currentWeight) +
            (4.7 * this.profile.feet*12+ +this.profile.inches) - (4.7 * 23)
            * this.profile.activityLevel + (this.profile.timeline * 1))
            
        }
        else {
            return Math.trunc(66 + (6.23 * this.profile.currentWeight) +
            (12.7 * this.profile.feet*12+ +this.profile.inches) - (6.8 * 23
            )* this.profile.activityLevel + (this.profile.timeline * 1))
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
            return item.mealType === 'Breakfast';
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

    .water-container {
        width: 30%;
    }

    .macros {
        width: 70%;
        margin-right: 20px;
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
 .glass-container {
        display: flex;
        justify-content: space-between;
    }

    .bars-container {
        display: flex;
    }

    .circle-container {
        display: flex;
        justify-content:space-evenly;
    }

    .remove-food, .edit-food {
        padding-left: 50px;
    }

    i.far.fa-edit {
        color: black;
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