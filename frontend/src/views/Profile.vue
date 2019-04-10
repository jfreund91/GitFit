<template>
    <div id="profile">
        <h1><i class="fas fa-heartbeat h1-img"></i> Profile</h1>
        <div class="form">
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
                <span class="label">Height:</span>
                <input type="text" v-model="profile.height.feet" placeholder="ft">
                <input type="text" v-model="profile.height.inches" placeholder="inches" id="inches">
            </div>
            <div class="form-input">
                <span class="label" id="activity-level">Activity Level:</span>
                <input type="radio" v-model="profile.activityLevel" value="0">Sedentary
                <input type="radio" v-model="profile.activityLevel" value="1">Lightly Active
                <input type="radio" v-model="profile.activityLevel" value="2">Moderately Active
                <input type="radio" v-model="profile.activityLevel" value="3">An Exercise Beast
                <!-- <span>value: {{profile.activityLevel}}</span> -->
            </div>
            <button :disabled="!isValidForm" v-on:click="saveProfile">GitFit!</button>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            profile: {
                age: '',
                currentWeight: '',
                goalWeight: '',
                height: {
                    feet: '',
                    inches: ''
                },
                activityLevel: ['Sedentary', 'Lightly Active', 'Moderately Active', 'An Exercise Beast' ]
            }
        }
    },
    methods: {
        saveProfile() {
            // Add logic to save profile to an array or a generic user on the database

            // Redirect the user to the tracking page
            this.$router.push('Tracking')

            // this.reviewID === 0 ? this.createReview() : this.updateReview();
        },

        // Populate a numbers drop-down
        populateSelect(target, min, max){
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
       && (this.profile.activityLevel === '0' || this.profile.activityLevel === '1' || this.profile.activityLevel === '2' || this.profile.activityLevel === '3');
    },
  },
}

</script>

<style>

@import url('https://fonts.googleapis.com/css?family=Kodchasan');

#profile {
    padding: 2%;
    font-family: 'Kodchasan', sans-serif;
    text-align: center;
}

.form-input {
    margin: 15px 0px;
}

.form-input .label {
    margin-right: 8px;
}

#inches {
    margin-left: 5px; 
}

#activity-level {
    margin-right: 10px;
}

span.label {
    text-transform: uppercase;
    font-weight: 530;
    color: #010913;
}

h1 {
    /* color: #777; */
    color: whitesmoke;
    text-shadow: 1px 1px 2px black, 0 0 25px #777, 0 0 5px darkblue;
    font-size: 40px;
    letter-spacing: 2px;
}

button {
    margin-top: 10px;
}

.h1-img {
    color: rgb(216, 48, 48);
}

select {
    font-size: 16px;
}

</style>