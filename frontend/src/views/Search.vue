<template>
<div>
    <h1 id="search-header">Search</h1>
    <div class="container">
        <div id="search-bar">
            <input class="search" type="text" name="queryString" v-model="queryString" @keyup.enter="searchFoods()">
            <input class="search-btn" type ="submit" value="Search" @click="searchFoods()">
        </div>
        <!-- Display "favorites" or frequently used foods here -->
        <!-- Every time a user adds a food item, it should go into favories -->
        <!-- This should pull from those favorites and populate a list -->
        <h2 id="favorite-foods" v-if="(isAuthenticated)">Favorites</h2>
        <div class="search-results">
            <ul>
                <li v-for="item in this.favoriteResults" :key="item.ndbno">
                <span @click="viewDetail(item.ndbno)" class="i-span"><i class="fas fa-plus-circle"></i><span class="item-text">{{item.name}}</span></span>
                
                </li>
            </ul>
        </div>

<!-- Search Results -->
<h2 v-if="searchResults.length !== 0" id="search-results">Search Results</h2>
        <div class="search-results">
            <ul>
                <li v-for="item in this.searchResults" :key="item.ndbno">
                <!-- Click plus to see detail view of an item -->
                <span @click="viewDetail(item.ndbno)" class="i-span"><i class="fas fa-plus-circle"></i><span class="item-text">{{item.name}}</span></span>
                </li>
            </ul>
        </div>

<!-- Modal to have details pop-up -->
        <modal name="food-item-detail-view" :height="350">
        <!-- Gives the detailed view -->
            <div >
                <h3 id="detail-food-view-header">{{detailItem.name}}</h3>
                    <div class = "detail-food-view">
                    <h4>Nutritional Value per Serving</h4>
                    <h5>Serving Size: {{detailItem.servingRate * 100}}g</h5>
                    <div id="food-specs">
                        <div>{{Math.trunc(detailItem.calories) * detailItem.servings}} Calories</div>
                        <div>{{Math.trunc(detailItem.fat) * detailItem.servings}}g Fat</div>
                        <div>{{Math.trunc(detailItem.carbs ) * detailItem.servings}}g Carbs</div>
                        <div>{{Math.trunc(detailItem.protein) * detailItem.servings}}g Protein</div>
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
                    <button id="i-ate-this-btn" @click="addFood()">I ate this!</button>
                    <button id="not-this-one-btn" value="No, not this one!" @click="hide()">No, not this one!</button>
                </div>
                </div>
            </div>
        </modal>


    </div>
</div>
</template>

<script>
import auth from '@/shared/auth.js'
let user = auth.getUser();



export default {
 name: 'search',
 data() {
     return {
         isAuthenticated: auth.getUser() !== null,
         queryString: "",
         searchResults : [],
         favoriteResults : [],
         detailItem: {
             ndbno: 0,
             name: "",
             calories: 0,
             fat: 0,
             carbs: 0,
             protein: 0,
             servings: 0,
             mealType: '',
             servingRate: 1,
         }
     }
 },
 created() {
     let userInCreated = auth.getUser();
        if(userInCreated !== null) {
        fetch(`${process.env.VUE_APP_REMOTE_API}/favorites`, {
                    method: 'GET',
                    headers: {
                            "Authorization": 'Bearer ' + auth.getToken() 
                            }, 
                    })
                    .then((response) => {
                    return response.json();
                    }).then ((json) => {
                        this.favoriteResults = [];
                        json.forEach(item => {
                        this.favoriteResults.push({
                            name: item.name,
                            calories: item.calories,
                            fat: item.fat,
                            protein: item.protein,
                            carbs: item.carbs,
                            ndbno: item.ndbno,
                            servings: '1',
                            mealType: 'Snack'
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
     searchFoods() {
         fetch(`https://api.nal.usda.gov/ndb/search/?format=json&ds=Standard%20Reference&q=${this.queryString}&max=25&offset=0&api_key=V0RN5a4cjw39PHwdYDOTobVDhOad60hDqVHF0NJl`,{
             method: 'GET',
             headers : { 
            'Content-Type': 'application/json',
            'Accept': 'application/json'
       }}).then(response => response.json()).then(json => {
             this.searchResults = json.list.item;
             //this.showSearch = true;
             });
     },
     viewDetail(itemDbNo) {
         this.$modal.show('food-item-detail-view');
         fetch(`https://api.nal.usda.gov/ndb/reports/?ndbno=${itemDbNo}&type=b&format=JSON&api_key=V0RN5a4cjw39PHwdYDOTobVDhOad60hDqVHF0NJl`, {
             method: 'GET',
             headers: {
                 'Content-Type': 'application/json',
                 'Accept': 'application/json'
             }}).then(response => response.json()).then(json => {
                 this.detailItem.servingRate = json.report.food.nutrients[0].measures[0].eqv / 100;
                 this.detailItem.ndbno = json.report.food.ndbno;
                 this.detailItem.name = json.report.food.name;
                 this.detailItem.calories = json.report.food.nutrients[1].value *this.detailItem.servingRate;
                 this.detailItem.protein = json.report.food.nutrients[2].value * this.detailItem.servingRate;
                 this.detailItem.fat = json.report.food.nutrients[3].value * this.detailItem.servingRate;
                 this.detailItem.carbs = json.report.food.nutrients[4].value * this.detailItem.servingRate;
                 console.log(this.detailItem)
                 
                //  this.showSearch = false;
             });
        },
        addFood() {
            if (user === null) {
            this.profile.eatenToday.push(
                {
                    id: this.profile.eatenToday.length + 1,
                    name: this.detailItem.name,
                    calories: Math.trunc(this.detailItem.calories * this.detailItem.servings),
                    fat: this.detailItem.fat * this.detailItem.servings,
                    carbs: this.detailItem.carbs * this.detailItem.servings, 
                    protein: this.detailItem.protein * this.detailItem.servings,
                    mealType: this.detailItem.mealType,
                    servings: this.detailItem.servings
                    //mealType: this.detailItem.mealType * this.detailItem.servings
                }
            ) 
            this.$router.push('/tracking');
            } 
            
            // User is is logged in, Add to database and also favorites
            else if(user !== null)
            {   // Add to database
                fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/addfood`, {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                },
                body: JSON.stringify(this.detailItem)
                }).then(response => {
                    if(response.ok) {
                        this.$router.push('/tracking');
                    }
                });

                // // Add to favorites
                // fetch(`${process.env.VUE_APP_REMOTE_API}/favorites`, {
                //     method: "POST",
                //     headers: {
                //         'Content-Type': 'application/json',
                //         Authorization: "Bearer " + auth.getToken()
                //     },
                //     body: JSON.stringify(this.detailItem)
                //     }).then(response => {
                //     if(response.ok) {
                //         this.$router.push('/tracking');
                //     }
                // });


            }
            
        }
    }
}
</script>
<style>

    .search-results ul {
        color: black;
        /* text-align: right; */
    }

    .search-results li:nth-of-type(even){
        background-color: #f7f7f7;
        padding: 7px 0px;
    }

    i {
        color: green;
        font-size: 30px;
    }

    i:hover {
        cursor: pointer;
    }

    .detail-food-view li{
        list-style: none;
    }

    .detail-food-view ul {
        padding-left: 0px;
    }

    input {
        padding: 10px;
    }

    .container {
        margin: 50px auto 0px auto;
        width: 80%;
        padding-bottom: 40px;
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
        padding-bottom: 0px;
    }

    .search-results {
        margin-top: 50px;
    }

#search-header {
    text-align: center;
    padding-top: 30px;
}
#search-bar {
    text-align: center;
}

.detail-food-view {
    padding: 0px 10px;
}


#detail-food-view-header {
    /* color:rgb(58, 151, 151); */
    color: #f7f7f7;
    padding: 15px;
    /* background-color: #444; */
    background-color: #76a627;
    width: 100%;
    margin: 0;
}

#food-specs div:nth-child(even) {
    background-color: #f7f7f7;
}

#food-specs {
    border: 1px solid #555;
    margin-right: 10px;
}

#i-ate-this-btn {
    margin-left: 15px;
    width: 100px;
    padding: 5px;
}

#servings-detail {
    margin-top: 10px;
}

#not-this-one-btn {
    margin-left: 15px;
    width: 150px;
    padding: 5px;

}

.item-text {
    color: black;
    margin-left: 20px;
    
}

</style>