<template>
<div>
    <header-logged-out></header-logged-out>
    <div class="container">
        <input class="search" type="text" name="queryString" v-model="queryString">
        <input class="search-btn" type ="submit" value="Search" @click="searchFoods()">
        <div class="search-results">
            <ul>
                <li v-for="item in this.searchResults" :key="item.ndbno">{{item.name}}<span><i class="fas fa-plus-circle"></i></span></li>
            </ul>
        </div>
    </div>
    </div>
</template>
<script>
import HeaderLoggedOut from '../layout/HeaderLoggedOut.vue'

export default {
  name: 'tracking',
  components: {
      HeaderLoggedOut
  },
 data() {
     return {
         queryString: "",
         searchResults : [],
     }
 },
 methods: {
     searchFoods(queryString) {
         fetch(`https://api.nal.usda.gov/ndb/search/?format=json&ds=Standard%20Reference&q=${this.queryString}&max=25&offset=0&api_key=V0RN5a4cjw39PHwdYDOTobVDhOad60hDqVHF0NJl`,{
             method: 'GET',
             headers : { 
            'Content-Type': 'application/json',
            'Accept': 'application/json'
       }}).then(response => response.json()).then(json => {
             console.log(json.list.item);
             this.searchResults = json.list.item;
             });
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

</style>