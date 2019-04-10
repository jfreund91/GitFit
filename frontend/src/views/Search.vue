<template>
<div>
    <div class="container">
        <input class="search" type="text" name="queryString" v-model="queryString">
        <input class="search-btn" type ="submit" value="Search" @click="searchFoods()">
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
            <button @click="()=>{this.showSearch = true}">No, not this one!</button>
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
         detailItem: {
             name: "",
             kcal: 0,
             fat: 0,
             carbs: 0,
             protein: 0,
             servingRate: Number
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
             console.log(json.list.item);
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
                    name: this.detailItem.name,
                    kcal: this.detailItem.kcal * this.detailItem.servingRate
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

    input {
        padding: 10px;
    }

    .container {
        margin: 100px auto 0px auto;
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

</style>