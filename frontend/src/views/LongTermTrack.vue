<template>
    <div id="long-term-tracking">
        <h1>Long Term Tracking</h1>
        <div class="btn-container">
        <button id="weekly" v-on:click="getWeekly">Weekly</button>
        <button id="monthly" v-on:click="getMonthly">Monthly</button>
        <button id="yearly" v-on:click="getYearly">Annually</button>
        </div>
        <div id="tracking-list">
            <h2>{{tracking}}</h2>
            <div id="tracking-graph">
            <line-chart v-if="(weekly)" 
                :data="chartData"
                xtitle="" ytitle="Calories"
                ></line-chart>

            <line-chart v-if="(monthly)" :data="chartData" ytitle="Calories"></line-chart>
            <line-chart v-if="(yearly)" :data="chartData" ytitle="Calories"></line-chart>
            <h2>Daily Goal: {{calorieBudget}} Calories</h2>
            <h2 v-if="(weekly)">Daily Average: {{dailyAverage}}</h2>
            <h2 v-if="(monthly)">Daily Average: {{dailyAverage}}</h2>
            <h2 v-if="(yearly)">Daily Average: {{dailyAverage}}</h2>
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
             chartData: [],
             secondSeries: []
        }
    },
    methods: {
        getWeekly(){
            this.secondSeries = [];
            this.chartData = [];
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
             console.log(this.results);
             this.getChartData();
             this.getSecondSeriesData();
             });
             },
        getMonthly() {
             this.chartData = [];
             this.secondSeries = [];
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
             console.log(this.results);
             this.getChartData();
             this.getSecondSeriesData();
             });

        },
        getYearly() {
            this.secondSeries = [];
            this.chartData = [];
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
             console.log(this.results);
             this.getChartData();
             this.getChartData();
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
        },
            getChartData() {
            for(let i = 0; i<this.results.length; i++ ) {
                let day = i + 1;
                const foo = `Day ${day}`;
                if(this.results[i] > 0 || day === 1 || day === this.results.length){
                    this.chartData.push( [foo, this.results[i] ])
                }
            }
        },
            getSecondSeriesData() {
                for(let i = 0; i<this.results.length; i++ ) {
                let day = i + 1;
                const foo = `Day ${day}`;
                if(this.results[i] > 0 || day === 1 || day === this.results.length){
                this.secondSeries.push( [foo, calorieBudget() ])
                }
            }
            }
    },
    computed: {
        calorieBudget () {
        if(this.profile.gender === 'F') { 
            return Math.trunc(655 + (4.35 * this.profile.currentWeight) +
            (4.7 * this.profile.feet*12+ +this.profile.inches) - (4.7 * 23)
            * this.profile.activityLevel + (this.profile.timeline * 1))
            
        }
        else {
            return Math.trunc(66 + (6.23 * this.profile.currentWeight) +
            (12.7 * this.profile.feet*12+ +this.profile.inches) - (6.8 * 23
            )* this.profile.activityLevel + (this.profile.timeline * 1))
        }
    },
    dailyAverage() {
        let dailyCalAvg = 0;
        for(let i=0; i<this.results.length; i++) {
            dailyCalAvg += this.results[i];
        }
        dailyCalAvg =  Math.trunc(dailyCalAvg/this.results.length);
        return dailyCalAvg;
    }
}
}
</script>
<style>
    #long-term-tracking {
        width: 80%;
        margin: 0 auto;
        padding-bottom: 60px;
    }

    #weekly, #monthly, #yearly, #lifetime {
        width: 200px;
        padding: 10px 0px;
    }

    #tracking-list h2 {
        text-align: center;
    }

    #tracking-graph {
        margin-bottom: 50px;
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