<template>
    <div id="long-term-tracking">
        <h1>Long Term Tracking</h1>
        <div class="btn-container">
        <button id="weekly">Weekly</button>
        <button id="monthly">Monthly</button>
        <button id="yearly">Annually</button>
        <button id="lifetime">Lifetime</button>
        </div>
        <div id="tracking-list">

        </div>
    </div>
</template>
<script>
export default {
    data(){
        return {
            tracking: ""
        }
    },
    methods: {
        getWeekly(){
            let weekly = [];
            tracking = "Weekly";
            return weekly;
        }, 
        getMonthly() {
            let monthly = [];
            tracking = "Monthly"; 
            return monthly;
        },
        getYearly() {
            let yearly = [];
            tracking = "Yearly";
            return yearly;
        },
        getLifetime() {
            let lifetime = [];
            tracking = "Lifetime";
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

    .btn-container {
        display: flex;
        justify-content: space-between;
    }

    #long-term-tracking h1 {
        text-align: center;
    }
</style>