<template>
    <div id="long-term-tracking">
        <h1>Long Term Tracking</h1>
        <div class="btn-container">
        <button id="weekly" v-on:click="getWeekly">Weekly</button>
        <button id="monthly" v-on:click="getMonthly; getChartData;">Monthly</button>
        <button id="yearly" v-on:click="getYearly">Annually</button>
        <button id="lifetime" v-on:click="getLifetime">Lifetime</button>
        </div>
        <div id="tracking-list">
            <h2>{{tracking}}</h2>
            <div id="tracking-graph">
            <line-chart v-if="(weekly)" :data="{'Day 1': this.results[0] , 'Day 2': this.results[1], 'Day 3': this.results[2], 'Day 4': this.results[3], 'Day 5': this.results[4], 'Day 6': this.results[5], 'Day 7': this.results[6] }"></line-chart>
            <line-chart v-if="(monthly)" :data="chartData"></line-chart>
            <line-chart v-if="(yearly)" :data="{'2017-05-13': 2, '2017-05-14': 5}"></line-chart>
        </div>
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
             weekly: false,
             monthly:false,
             yearly: false,
             chartData: []
        }
    },
    methods: {
        getWeekly(){
            this.tracking = "This Week";
            this.weekly = true;
            this.monthly = false;
            this.yearly = false;
             fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/weeklycals`, {
                 method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + auth.getToken()
                }
             }).then(response => response.json()).then(json => {
             this.results = json;
             });
             },
        getMonthly() {
            this.tracking = "This Month"; 
            this.monthly = true;
            this.weekly =false;
            this.yearly = false;
            this.results = [];
             fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/monthlycals`, {
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
            this.weekly = false;
            this.monthly = false;
             fetch(`${process.env.VUE_APP_REMOTE_API}/tracking/yearlycals`, {
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
    computed: {
        getChartData() {
            for(let i = 0; i<this.results.length; i++ ) {
                let day = i + 1;
                const foo = `Day ${day}`;
                this.chartData.push( [foo, this.results[i] ])
            }
            console.log(chartData); 
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