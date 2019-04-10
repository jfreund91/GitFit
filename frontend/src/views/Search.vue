<template>
<div>
    <h1 id="search-header">Search</h1>
    <div class="container">
        <div id="search-bar">
            <input class="search" type="text" name="queryString" v-model="queryString" @keyup.enter="searchFoods()">
            <input class="search-btn" type ="submit" value="Search" @click="searchFoods()">
        </div>
        <div v-if="showSearch" class="search-results">
            <ul>
                <li v-for="item in this.searchResults" :key="item.ndbno">{{item.name}}
                    <span @click="viewDetail(item.ndbno)"><i class="fas fa-plus-circle"></i></span>
                </li>
            </ul>
        </div>
        <div class = "detail-food-view" v-if="! showSearch">
            <h2>{{detailItem.name}}</h2>
            <ul>
                <h3>Nutritional Value per Serving</h3>
                <li>{{Math.trunc(detailItem.kcal * detailItem.servingRate)}} Calories</li>
                <li>{{Math.trunc(detailItem.fat  * detailItem.servingRate)}}g Fat</li>
                <li>{{Math.trunc(detailItem.carbs * detailItem.servingRate)}}g Carbs</li>
                <li>{{Math.trunc(detailItem.protein * detailItem.servingRate)}}g Protein</li>
            </ul>
            <button value="No, not this one!" @click="()=>{this.showSearch = true}">No, not this one!</button>
             <label><strong> Servings: </strong></label>
             <select v-model="detailItem.servingsConsumed">
                <option value="0.5">1/2</option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
            </select>
            <label><strong> Meal: </strong></label>
            <select v-model="meal">
                <option value="Snack">Snack</option>
                <option value="Breakfast">Breakfast</option>
                <option value="Lunch">Lunch</option>
                <option value="Dinner">Dinner</option>
            </select>
            <button @click="addFood()">I ate this!</button>
           
        </div>
    </div>
</div>
</template>

<script>
export default {
  name: 'search',
 data() {
     return {
         showSearch: true,
         queryString: "",
         searchResults : [],
         meal: '',
         detailItem: {
             name: "",
             kcal: 0,
             fat: 0,
             carbs: 0,
             protein: 0,
             servingRate: Number,
             servingsConsumed: 0,
         }
     }
 },
 methods: {
     searchFoods() {
         fetch(`https://api.nal.usda.gov/ndb/search/?format=json&ds=Standard%20Reference&q=${this.queryString}&max=25&offset=0&api_key=V0RN5a4cjw39PHwdYDOTobVDhOad60hDqVHF0NJl`,{
             method: 'GET',
             headers : { 
            'Content-Type': 'application/json',
            'Accept': 'application/json'
       }}).then(response => response.json()).then(json => {
             this.searchResults = json.list.item;
             this.showSearch = true;
             });
     },
     viewDetail(itemDbNo) {
         fetch(`https://api.nal.usda.gov/ndb/reports/?ndbno=${itemDbNo}&type=b&format=JSON&api_key=V0RN5a4cjw39PHwdYDOTobVDhOad60hDqVHF0NJl`, {
             method: 'GET',
             headers: {
                 'Content-Type': 'application/json',
                 'Accept': 'application/json'
             }}).then(response => response.json()).then(json => {
                 this.detailItem.name = json.report.food.name;
                 this.detailItem.kcal = json.report.food.nutrients[1].value;
                 this.detailItem.protein = json.report.food.nutrients[2].value;
                 this.detailItem.fat = json.report.food.nutrients[3].value;
                 this.detailItem.carbs = json.report.food.nutrients[4].value;
                 this.detailItem.servingRate = json.report.food.nutrients[0].measures[0].eqv/100;
                 this.showSearch = false;
             });
        },
        addFood() {
            this.profile.eatenToday.push(
                {
                    id: this.profile.eatenToday.length + 1,
                    name: this.detailItem.name,
                    kcal: this.detailItem.kcal * this.detailItem.servingRate * this.detailItem.servingsConsumed,
                    fat: this.detailItem.fat * this.detailItem.servingRate * this.detailItem.servingsConsumed,
                    carbs: this.detailItem.carbs * this.detailItem.servingRate * this.detailItem.servingsConsumed,
                    protein: this.detailItem.protein * this.detailItem.servingRate * this.detailItem.servingsConsumed,
                    meal: this.meal
                }
                
            );
            this.$router.push('/tracking')
        }
    }
}
</script>
<style>

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

</style>