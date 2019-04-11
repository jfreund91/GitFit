<template>
    <div id="profile">
        <img src="../assets/action-athlete-barbell.jpg"> 
        <div id="profile-section">
        <h1><i class="fas fa-heartbeat h1-img"></i> Profile</h1>
        <div class="form">
            <div class="form-input">
                <span class="label">Name:</span>
                <input type="text" v-model="profile.nameOfUser" placeholder="Enter Your Name">
            </div>
            <div class="form-input">
                <span class="label">Age:</span>
                <!-- <select name="age" id="age" v-on:click="populateSelect('age', 9, 125)">
                </select> -->
                <input type="text" v-model="profile.age" placeholder="Enter Your Age">
            </div>
            <div class="form-input">
                <span class="label">Current Weight:</span>
                <input type="text" v-model="profile.currentWeight" placeholder="Current Weight">
            </div>
            <div class="form-input">
                <span class="label">Goal Weight:</span>
                <input type="text" v-model="profile.goalWeight" placeholder="Goal Weight">
            </div>
            <div class="form-input">
                <span class="label">Goal Timeline:</span>
                <select name="goal-timeline" v-model="profile.timeline" class="goal-timeline">
                    <option value="-998">Lose Weight: (-2.0 lbs/week)</option> <!-- 998 calorie deficit per day -->
                    <option value="-748">Lose Weight: (-1.5 lbs/week)</option>
                    <option value="-499">Lose Weight: (-1 lbs/week)</option>
                    <option value="-249">Lose Weight: (-0.5 lbs/week)</option>
                    <option value="0">Maintain Weight</option>
                    <option value="+249">Gain Weight: (+0.5 lbs/week)</option>
                    <option value="+499">Gain Weight: (+1.0 lbs/week)</option>
                    <option value="+748">Gain Weight: (+1.5 lbs/week)</option>
                    <option value="+998">Gain Weight: (+2.0 lbs/week)</option>
                </select>
            </div>
            <div class="feet-inches">
                <span class="label">Height:</span>
                <div>
                    <input type="text" v-model="profile.height.feet" placeholder="" id="feet"> ft
                    <input type="text" v-model="profile.height.inches" placeholder="" id="inches"> in
                </div>
            </div>
            <div class="form-input">
                <span class="label">Gender:</span>
                <select name="gender" id="gender" v-model="profile.gender">
                    <option value="M">Male</option>
                    <option value="F">Female</option>
                </select>
            </div>
            <div class="form-input">
                <span class="label" id="activity-level">Activity Level:</span>
                <input type="radio" v-model="profile.activityLevel" value="1.2" title="Little or no exercise, desk job"><span title="Little or no exercise, desk job">Sedentary</span>
                <input type="radio" v-model="profile.activityLevel" value="1.375" title="Light exercise/Sports 1-3 days/week"><span title="Light exercise/Sports 1-3 days/week">Lightly Active</span>
                <input type="radio" v-model="profile.activityLevel" value="1.55" title="Moderate exercise/Sports 6-7 days/week"><span title="Moderate exercise/Sports 6-7 days/week">Moderately Active</span>
                <input type="radio" v-model="profile.activityLevel" value="1.9" title="My calls are forwarded to the gym/Hard exercise 2 or more times per day, 
                or training for marathon, or triathon">
                <span title="My calls are forwarded to the gym/Hard exercise 2 or more times per day, 
                or training for marathon, or triathon">An Exercise Beast</span>
                <!-- <span>value: {{profile.activityLevel}}</span> -->
            </div>
            <button :disabled="!isValidForm" v-on:click="saveProfile">GitFit!</button>
        </div>
        </div>
    </div>
</template>

<script>
import Vue from 'vue'
import persistentState from 'vue-persistent-state'
 
let initialState = {
    profile: {
        age: '',
        currentWeight: '',
        goalWeight: '',
        height: {
        feet: '',
        inches: ''
        },
        gender: '',
        activityLevel: ['Sedentary', 'Lightly Active', 'Moderately Active', 'An Exercise Beast' ],
        timeline: '',
        nameOfUser: '',
        eatenToday: []
    }
}
 
Vue.use(persistentState, initialState)
// InitialState is injected as data in all vue instances
// Any changes to state will be stored in localStorage

export default {
    data() {
        return {

        }
    },
    methods: {
        saveProfile() {
            // Redirect the user to the tracking page
            this.$router.push('Tracking')

            // this.reviewID === 0 ? this.createReview() : this.updateReview();
        },

        // Populate a numbers drop-down
        populateSelect(target, min, max) {
            let select, i, option;
            select = document.getElementById(target);
            for ( i = min; i <= max; i += 1 ) {
                option = document.createElement('option');
                option.value = option.text = i;
                select.add(option);
            }
        },
    },
    // Check that all of the profile form fields are filled out
    computed: {
    isValidForm() {
      return this.profile.age != '' && this.profile.currentWeight != '' && this.profile.goalWeight != ''
       && this.profile.height.feet != '' && this.profile.height.inches != '' 
       && (this.profile.activityLevel === '1.2' || this.profile.activityLevel === '1.375' || this.profile.activityLevel === '1.55' || this.profile.activityLevel === '1.9')
       && this.profile.gender != '' && this.profile.timeline != '' && this.profile.nameOfUser != '';
    },
  },
}

</script>

<style>


#profile {
    padding: 2%;
    margin: 50px auto;
    text-align: center;
    padding-top: 0px;
    display: flex;
    margin-left: 10px;
    justify-content: center;
    margin-right: 10px;
    
}

#profile img {
    width: 570px;
    height: 570px;
    border: solid 2px red;
    margin-right: 30px;
    margin-top: 30px;
}

.form-input {
    margin: 15px 0px;
    display: flex;
    justify-content: space-between;
}

.feet-inches {
    margin: 15px 0px;
    display: flex;
    justify-content: space-between;
}

.form-input .label {
    margin-right: 8px;
}

#feet, #inches {
    width: 20px;
    border: solid black 1px;
    margin-left: 20px;
}

#activity-level {
    margin-right: 10px;
}

span.label {
    font-weight: 530;
    color: #010913;
    align-self: center;
}

h1 {
    /* color: #777; */
    color: whitesmoke;
    text-shadow: 1px 1px 2px black, 0 0 25px #777, 0 0 5px darkblue;
    font-size: 40px;
    letter-spacing: 2px;
}

button, a {
    margin-top: 10px;
    cursor: pointer;
    width: 100%;
}

.h1-img {
    color: rgb(216, 48, 48);
}

select {
    font-size: 16px;
}

.form {
    margin-bottom: 30px;
    max-width: 500px;
}

#gender {
    height: 35px;
}

.goal-timeline {
    height: 35px;
    width: 215px;
    /* font-size: 17px; */
}



</style>