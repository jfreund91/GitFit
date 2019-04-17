<template>
    <div id="long-term-tracking">
        <h1>Long Term Tracking</h1>
        <div class="btn-container">
        <button id="weekly" v-on:click="getWeekly">Weekly</button>
        <button id="monthly" v-on:click="getMonthly">Monthly</button>
        <button id="yearly" v-on:click="getYearly">Annually</button>
        <button id="lifetime" v-on:click="getLifetime">Lifetime</button>
        </div>
        <div id="tracking-list">
            <h2>{{tracking}}</h2>
            <ul>
                <li v-for="item in this.results" :key="item.ndbno"></li>
            </ul>
        </div>
    </div>
</template>
<script>
import auth from '@/shared/auth.js'
export default {
    data(){
        return {
             tracking : "",
             results: []
        }
    },
    methods: {
        getWeekly(){
            this.tracking = "This Week";
             fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/week`, {
                 method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                }
             }).then(response => response.json()).then(json => {
             this.results = json.list.item;
             });
             },
        getMonthly() {
            this.tracking = "This Month"; 
             fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/month`, {
                 method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                }
             }).then(response => response.json()).then(json => {
             this.results = json.list.item;
             });
             return this.results;
        },
        getYearly() {
            this.tracking = "This Year";
             fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/year`, {
                 method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                }
             }).then(response => response.json()).then(json => {
             this.results = json.list.item;
             });
             return this.results;
        },
        getLifetime() {
            this.tracking = "Lifetime";
             fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/life`, {
                 method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                }
             }).then(response => response.json()).then(json => {
             this.results = json.list.item;
             });
             return this.results;
        }
    }
}
</script>
<style>
    #long-term-tracking {
        width: 80%;
        margin: 0 auto;
    }

    #weekly, #monthly, #yearly, #lifetime {
        width: 200px;
        padding: 10px 0px;
    }

    #tracking-list h2 {
        text-align: center;
    }

    #tracking-list {
        margin: 50px 0px;
    }

    .btn-container {
        display: flex;
        justify-content: space-between;
    }

    #long-term-tracking h1 {
        text-align: center;
    }
</style>