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
                <li></li>
            </ul>
        </div>
    </div>
</template>
<script>
export default {
    data(){
        return {
             tracking : ""
        }
    },
    methods: {
        getWeekly(){
            let weekly = [];
            this.tracking = "Weekly";
            return weekly;
        }, 
        getMonthly() {
            let monthly = [];
            this.tracking = "Monthly"; 
            return monthly;
        },
        getYearly() {
            let yearly = [];
            this.tracking = "Yearly";
            return yearly;
        },
        getLifetime() {
            let lifetime = [];
            this.tracking = "Lifetime";
            return lifetime;
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