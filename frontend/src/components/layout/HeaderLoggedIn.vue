<template>
    <header>
        <div class="img-container">
        <img src="../../assets/logo.png">
        </div>
        <nav>
                <ul>
                    <li>Profile</li>
                    <li>Track</li>
                    <li>Logout</li>
                </ul>
        </nav>
    </header>
</template>
<script>
    export default {
        name: "header-logged-in"
    }
</script>
<style>
    header {
        display: flex;
        width: 100%;
        background-color: #1a174e;
        justify-content: space-between;
    }

    nav {
        align-self: center;
        display: flex;
        padding: 20px;
    }

    ul {
        padding-left: 0;
        list-style: none;
        color: white;
        display: flex;
    }

    li {
        margin-left: 35px;
    }

    img { 
        width: 100%;
        height: auto;
    }

    .img-container {
        width: 25%;
    }

</style>