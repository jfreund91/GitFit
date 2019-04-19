<template>
    <div id="tracking">
        <h1 id="tracking-header">Track Calories</h1>
        <!-- Add gold stars here -->
        <h3 v-if="getNumberOfStars > 1" id="gold-stars">Gold Stars: 
            <span v-for="item in getNumberOfStars" :key="item">
            <img src="../assets/gold-star-transparent.png">
            </span>
        </h3>
        <h3 v-if="(isAuthenticated)" id="track-date">Date:  <input type="date" v-model="pullDate" 
        id="foodEatenDate" name="foodEatenDate" v-on:change="updateDate(pullDate + 'T00:00')"></h3>
        <div class="container circle-container">
            <div id="profile-stats">
                <h3>User Stats</h3>
                <div>Age: {{calculateAge}}</div>
                <div>Current Weight: {{profile.currentWeight}}</div>
                <div>Goal Weight: {{profile.goalWeight}}</div>
                <div>Height(in): {{profile.feet*12+ +profile.inches}}</div>
                <button id="removeLastEntry" @click="removeLastEntry(), getNumberOfStars()">Remove last entry</button>
                <router-link to="/long-term-tracking"><button v-if="(isAuthenticated)">View Long Term Tracking</button></router-link>
            </div>
            <!-- The Calories consumed bars -->
            <column-chart :data="[
                ['Calories Consumed', Math.trunc(caloriesConsumed)], 
                ['Calorie Budget', Math.trunc(calorieBudget - caloriesConsumed) ]
            ]" :colors="[['#b00', 'rgba(199, 100, 170, 0.99)'],['#b00', '#aaa']]"></column-chart>
            <!-- color of first bar, color of 2nd bar -->
        </div>


        <!-- Graph for tracking calories: goal/actual based on date -->
        <!-- <div id="tracking-calories-graph">
                <h2 id="track-calories-header">Track Calories Comparison - Goal/Actual</h2>
                <div id="track-cals-line">
                    <line-chart 
                        :data = "[
                        {name: 'Goal', data: {'2017-01-01 00:00:00 -0800': 3, '2017-01-02 00:00:00 -0800': 4, '2017-01-05 00:00:00 -0800': 4}},
                        {name: 'Actual', data: {'2017-01-01 00:00:00 -0800': 5, '2017-01-02 00:00:00 -0800': 3, '2017-01-05 00:00:00 -0800': 4}}]"
                        xtitle="Date" ytitle="Calories"
                    ></line-chart>
                </div>
        </div> -->


        <div class="container bars-container">
            <div class="macros">
                <h1>% Based On FDA Recommended Value</h1>
                <bar-chart suffix="%" 
                    :data="[
                        ['Protein', Math.trunc((this.proteinConsumed/50)*100)], 
                        ['Carbs', Math.trunc((this.carbsConsumed/300)*100)], 
                        ['Fat', Math.trunc((this.fatConsumed/80)*100)]
                    ]" :colors="[['orange', 'rgba(23, 46, 170, 0.99)', 'purple'],['#b00', '#aaa']]"
                    >
                    </bar-chart>
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
            <!------------------------ SNACKS ------------------------>
            <div>
                <div class="meal-list-header">
                    <h2>Snacks</h2>
                </div>
                <div class="container">
                    <router-link to="/search"><i class="fas fa-plus-circle large-plus"><span class="addfoodconsumed">Add Snack Food</span></i></router-link>
                    <table class="snacksTable">
                        <tr v-if="snacks.length !== 0">
                            <th>Description</th>
                            <th>Servings</th>
                            <th>Calories</th>
                        </tr>
                            <tr v-for="item in snacks" :key="item.id">
                                <td>
                                    {{item.name}}                               
                                </td>
                                <td>
                                    {{item.servings}}                               
                                </td>
                                <td>
                                    {{item.calories}}
                                    <span class="edit-food"  @click="viewDetail(item.id)"><i class="far fa-edit"></i></span>
                                    <span class="remove-food">
                                        <i class="fas fa-minus-circle large-minus" @click="removeFood(item.id), getNumberOfStars()"></i>
                                    </span>                                
                                </td>     
                            </tr>
                    </table>
                </div>
            </div>
            <!------------------------ BREAKFAST ------------------------>
            <div>                
                <div class="meal-list-header">
                    <h2>Breakfast</h2>
                </div>
                <div class="container">
                    <router-link to="/search"><i class="fas fa-plus-circle large-plus"><span class="addfoodconsumed">Add Breakfast Food</span></i></router-link>
                    <table class="snacksTable">
                        <tr v-if="breakfasts.length !== 0">
                            <th>Description</th>
                            <th>Servings</th>
                            <th>Calories</th>
                        </tr>
                            <tr v-for="item in breakfasts" :key="item.id">
                                <td>
                                    {{item.name}}                               
                                </td>
                                <td>
                                    {{item.servings}}                               
                                </td>
                                <td>
                                    {{item.calories}}
                                    <span class="edit-food"  @click="viewDetail(item.id)"><i class="far fa-edit"></i></span>
                                    <span class="remove-food">
                                        <i class="fas fa-minus-circle large-minus" @click="removeFood(item.id), getNumberOfStars()"></i>
                                    </span>                                
                                </td>     
                            </tr>
                    </table>
                </div>
            </div>
            <!------------------------ LUNCHES ------------------------>
            <div>                
                <div class="meal-list-header">
                    <h2>Lunch</h2>
                </div>
                <div class="container">
                    <router-link to="/search"><i class="fas fa-plus-circle large-plus"><span class="addfoodconsumed">Add Lunch Food</span></i></router-link>
                    <table class="snacksTable">
                        <tr v-if="lunches.length !== 0">
                            <th>Description</th>
                            <th>Servings</th>
                            <th>Calories</th>
                        </tr>
                            <tr v-for="item in lunches" :key="item.id">
                                <td>
                                    {{item.name}}                               
                                </td>
                                <td>
                                    {{item.servings}}                               
                                </td>
                                <td>
                                    {{item.calories}}
                                    <span class="edit-food"  @click="viewDetail(item.id)"><i class="far fa-edit"></i></span>
                                    <span class="remove-food">
                                        <i class="fas fa-minus-circle large-minus" @click="removeFood(item.id), getNumberOfStars()"></i>
                                    </span>                                
                                </td>     
                            </tr>
                    </table>
                </div>
            </div>
            <!------------------------ DINNER ------------------------>
            <div>
                <div class="meal-list-header">
                    <h2>Dinner</h2>
                </div>
                <div class="container">
                    <router-link to="/search"><i class="fas fa-plus-circle large-plus"><span class="addfoodconsumed">Add Dinner Food</span></i></router-link>
                    <table class="snacksTable">
                        <tr v-if="dinners.length !== 0">
                            <th>Description</th>
                            <th>Servings</th>
                            <th>Calories</th>
                        </tr>
                            <tr v-for="item in dinners" :key="item.id">
                                <td>
                                    {{item.name}}                               
                                </td>
                                <td>
                                    {{item.servings}}                               
                                </td>
                                <td>
                                    {{item.calories}}
                                    <span class="edit-food"  @click="viewDetail(item.id)"><i class="far fa-edit"></i></span>
                                    <span class="remove-food">
                                        <i class="fas fa-minus-circle large-minus" @click="removeFood(item.id), getNumberOfStars()"></i>
                                    </span>                                
                                </td>     
                            </tr>
                    </table>
                </div>
            </div>
        </div>
        <!-- Modal for the food item details -->
        <modal name="food-item-detail-view" :height="350">
        <!-- Gives the detailed view -->
            <div >
                <h3 id="detail-food-view-header">{{detailItem.name}}</h3>
                    <div class = "detail-food-view">
                    <h4>Nutritional Value per Serving</h4>
                    <div id="food-specs">
                        <div id="calories">{{Math.trunc(detailItem.calories) * detailItem.servings}} Calories</div>
                        <div id="fat">{{Math.trunc(detailItem.fat) * detailItem.servings}}g Fat</div>
                        <div id="carbs">{{Math.trunc(detailItem.carbs) * detailItem.servings }}g Carbs</div>
                        <div id="protein">{{Math.trunc(detailItem.protein) * detailItem.servings}}g Protein</div>
                    </div>
                <!-- <button value="No, not this one!" @click="()=>{this.showSearch = true}">No, not this one!</button> -->
                <div id="servings-detail">
                    <label><strong> Servings: </strong></label>
                    <select v-model="detailItem.servings">
                        <option value="0.5">1/2</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                    </select>
                    <label><strong> Meal: </strong></label>
                    <select v-model="detailItem.mealType">
                        <option value="Snack">Snack</option>
                        <option value="Breakfast">Breakfast</option>
                        <option value="Lunch">Lunch</option>
                        <option value="Dinner">Dinner</option>
                    </select>
                    <button id="i-ate-this-btn" @click="editFood(detailItem)">I ate this!</button>
                </div>
                </div>
            </div>
        </modal>
    </div>
</template>

<script>
import Vue from 'vue'
import VueChartkick from 'vue-chartkick'
import Chart from 'chart.js'
import auth from '@/shared/auth.js' // import whether user is logged in

let dateFormat = require('dateformat');
let now = new Date();

Vue.use(VueChartkick, {adapter: Chart})
export default {
    name: 'tracking',
    data() {
        return {
            isAuthenticated: auth.getUser() !== null,
            detailItem: {
             id: 0,
             ndbno: 0,
             name: "",
             calories: 0,
             fat: 0,
             carbs: 0,
             protein: 0,
             servings: 0,
             mealType: '',
             servingRate: 1,
            },
            pullDate: dateFormat(new Date(), "yyyy-mm-dd")
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
                    servings: item.servings,
                    date: item.date
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
        viewDetail(itemId) {
         this.$modal.show('food-item-detail-view'); // Opens the modal to edit the servings
         let result = [];
         result = this.profile.eatenToday.filter((item) => {
             return item.id === itemId;
         });
         this.detailItem.ndbno = result[0].ndbno;
         this.detailItem.name = result[0].name;
         this.detailItem.calories = result[0].calories;
         this.detailItem.fat = result[0].fat;
         this.detailItem.protein = result[0].protein;
         this.detailItem.carbs = result[0].carbs;
         this.detailItem.servings = result[0].servings;
         this.detailItem.mealType = result[0].mealType;
         this.detailItem.id = results[0].entryId;
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
                    return item.id !== foodId;
                 });
                fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/remove`, {
                method: "DELETE",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                },
                body: JSON.stringify(foodId)
                })
                this.profile.eatenToday = output;
            }
        },
        // Change food items based on date being updated
        updateDate(dateToDisplay) {
            // console.log(dateToDisplay) --2019-04-17T00:00
            console.log("Datttte:")
            console.log(dateToDisplay)
            let user = auth.getUser();
            if (user == null) { // Not logged in
            let output = [];
            output = this.profile.eatenToday.filter((item)=> {
                return item.date == dateToDisplay;
            });
            this.profile.eatenToday = output;
            } else if (user != null) { // User is logged in so pull from db
                let output = [];
                output = this.profile.eatenToday.filter((item)=> {
                    return item.date == dateToDisplay;
                 });
                fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/dailyfood`, {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                },
                body: JSON.stringify(dateToDisplay)
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
                    servings: item.servings,
                    date: item.date
                })})
            });
            }
            //this.profile.eatenToday = output;
            // console.log("The date is:")
            // console.log(this.profile.eatenToday[0].date)
            // console.log("The date in pull is")
            console.log(this.pullDate)
        },

        editFood(detailItem) {
            let user = auth.getUser();
            let arr = this.profile.eatenToday;
            for( let i = 0; i < arr.length; i++) { 
                    if ( arr[i].name === detailItem.name) {
                        arr[i].servings = detailItem.servings;
                        arr[i].mealType = detailItem.mealType;
                        if(user !== null){
                            let output = arr[i];
                            fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/updatefood`, {
                                method: "PATCH",
                                headers: {
                                    'Content-Type': 'application/json',
                                    Authorization: "Bearer " + auth.getToken()
                                },
                                body: JSON.stringify(output)
                                }).then(response => {
                                    if(response.ok) {
                                        console.log(response)
                                    }
                                })
                        }


                    }
            }
           
            this.hide();
                }
            },


    computed: {
        // Get the date
        getDate() {

        },
        calculateAge() {
        let DateOfBirth = new Date(this.profile.birthDate);
        let currentDate = new Date();
        let dateDiff = currentDate-DateOfBirth; // This is the difference in milliseconds
        let age = Math.floor(dateDiff/31557600000); // Divide by 1000*60*60*24*365.25

        return age;
    },
    calorieBudget () {
        if(this.profile.gender === 'F') { 
            return ((Math.trunc(655 + (4.35 * this.profile.currentWeight) +
            (4.7 * this.profile.feet*12+ +this.profile.inches) - (4.7 * 23))
            * this.profile.activityLevel) + (this.profile.timeline * 1))
            
        }
        else {
            return ((Math.trunc(66 + (6.23 * this.profile.currentWeight) +
            (12.7 * this.profile.feet*12+ +this.profile.inches) - (6.8 * 23))
            * this.profile.activityLevel) + (this.profile.timeline * 1))
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
    },
    getNumberOfStars() {
        let numOfStars = 0;

        // 2 stars
        if(
            (this.snacks.length  > 0 && this.breakfasts.length  > 0 ) || (this.breakfasts.length  > 0 && this.lunches.length  > 0 ) ||
            (this.lunches.length  > 0 && this.dinners.length  > 0 ) 
        ){  numOfStars = 2}

        // 3 stars
        if(
            (this.snacks.length  > 0 && this.breakfasts.length  > 0 && this.lunches.length > 0) || 
            (this.breakfasts.length  > 0 && this.lunches.length  > 0 && this.dinners.length > 0)
        ){ 
            numOfStars = 3}

        // 4 stars
        if(
            (this.snacks.length  > 0 && this.breakfasts.length  > 0 && this.lunches.length  > 0 && this.dinners.length  > 0 )
        ){ 
            numOfStars = 4}

        return numOfStars;
    },
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

#track-date, #gold-stars {
    text-align: center;
}

#track-calories-header {
    text-align: center;
}

#track-cals-line {
    padding: 0px 25%;
    width: 50%;
}

/* 
input[type="date"]::-webkit-inner-spin-button,
input[type="date"]::-webkit-calendar-picker-indicator {
    display: none;
    -webkit-appearance: none;
} */

input[type=date]::-webkit-clear-button {
    -webkit-appearance: none;
    display: none;
}

.snacksTable th {
    background-color: lightblue;
    text-align: left;
    border: solid black 1px;
}

.snacksTable {
    margin-top: 17px;
    border: solid black 1px;
}

.snacksTable td {
    border: solid midnightblue 2px;
}

.addfoodconsumed {
    color: black;
    font-size: 2rem;
    padding: 5px;
}

#gold-stars img {
    width: 50px;
    height: 50px;
}


</style>