<template>
    <div id="long-term-tracking">
        <h1>Long Term Tracking</h1>
        <div class="btn-container">
        <button id="weekly" v-on:click="getWeekly">Weekly</button>
        <button id="monthly" v-on:click="getMonthly">Monthly</button>
        <button id="yearly" v-on:click="getYearly">Annually</button>
        <button id="lifetime" v-on:click="getLifetime">Lifetime</button>
        </div>
        <div id="tracking-graph">
            <!-- <line-chart v-if="(weekly)" :data="{'Day 1': this.totalCalories , '2017-05-14': 5}"></line-chart>
            <line-chart v-if="(monthly)" :data="{'2017-05-13': 2, '2017-05-14': 5}"></line-chart>
            <line-chart v-if="(yearly)" :data="{'2017-05-13': 2, '2017-05-14': 5}"></line-chart>
            <line-chart v-if="(lifetime)" :data="{'2017-05-13': 2, '2017-05-14': 5}"></line-chart> -->
        </div>
        <div id="tracking-list">
            <h2>{{tracking}}</h2>
            <ul>
                <li v-for="item in this.results" :key="item.entryId">{{item.name}} Calories:{{item.calories}}</li>
            </ul>
            <h2 v-if="(calories)">{{this.totalCalories}}</h2>
        </div>
    </div>
</template>
<script>
import auth from '@/shared/auth.js'
import Vue from 'vue'
import VueChartkick from 'vue-chartkick'
import Chart from 'chart.js'
Vue.use(VueChartkick, {adapter: Chart})
export default {
    data(){
        return {
             tracking : "",
             results: [],
             totalCalories: 0,
             weekly: false,
             monthly:false,
             yearly: false,
             lifetime: false,
             calories: this.totalCalories > 0
        }
    },
    methods: {
        getWeekly(){
            this.tracking = "This Week";
            this.weekly = true;
             fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/week`, {
                 method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                }
             }).then(response => response.json()).then(json => {
             this.results = json;
             });
                for(let i = 0; i<this.results2; i++ ) {
                this.totalCalories += this.results2[i].calories;
            }
             },
        getMonthly() {
            this.tracking = "This Month"; 
            this.monthly = true;
             fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/month`, {
                 method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                }
             }).then(response => response.json()).then(json => {
             this.results = json;
             });

        },
        getYearly() {
            this.tracking = "This Year";
            this.yearly = true;
             fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/year`, {
                 method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                }
             }).then(response => response.json()).then(json => {
             this.results = json;
             });
        },
        getLifetime() {
            this.tracking = "Lifetime";
            this.lifetime = true;
             fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/lifetime`, {
                 method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                }
             }).then(response => response.json()).then(json => {
             this.results = json;
             });
        }
    },
    compute: {
        getTotalCalories() {
            for(let i = 0; i<this.results; i++ ) {
                this.totalCalories += this.results[0].item.calories;
            }
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