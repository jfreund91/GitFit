<template>
<div>
    <header-logged-out></header-logged-out>
    <div class="container">
        <input class="search" type="text">
        <input class="search-btn" type ="submit" value="Search">
        <div class="search-results">
            <ul>
                <li>Apple<span><i class="fas fa-plus-circle"></i></span></li>
                <li>Apple Sauce<span><i class="fas fa-plus-circle"></i></span></li>
                <li>Apple Juice<span><i class="fas fa-plus-circle"></i></span></li>
                <li>Apple Butter<span><i class="fas fa-plus-circle"></i></span></li>
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