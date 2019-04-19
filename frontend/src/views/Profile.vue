<template>
    <div id="profile">
        <!-- <img src="../assets/action-athlete-barbell.jpg" id="side-image">  -->
        <div class="img-container">
            <!-- Should be able to edit this -->
            <img class="profile-photo small" v-bind:src="profile.userImage" v-show="!(initialProfile)" />
                <!-- Show this if the user hasn't created a profile -->
            <img v-show="initialProfile" class="profile-photo small" src="https://res.cloudinary.com/gitfit/image/upload/v1555524869/true_gpo4zq.png" />
        </div>
        <div id="profile-section">
            <!-- Show the Change Password field if the user is logged in -->
            <!-- <a v-if="(isAuthenticated)" href="/changePassword">Change password</a> -->
            <router-link v-if="(isAuthenticated)" to="/ChangePassword">Change Password</router-link>
            <!-- Show the form if the user clicks change password -->
            <!-- <div class="form" v-show="ChangeUserPassword"> -->

        <h1><i class="fas fa-heartbeat h1-img"></i> Profile</h1>
        <div class="form">
            <div class="form-input">
                <h2>Upload profile photo</h2>
                    <vue-dropzone
                        id="dropzone"
                        v-bind:options="dropzoneOptions"
                        @vdropzone-file-added="afterComplete"
                        @vdropzone-sending="sending"
                        @vdropzone-success="success"
                    ></vue-dropzone>
                <!-- Show this if the user has already created a profile -->
                <!-- Should be able to edit this -->
                <!-- <img class="profile-photo small" v-bind:src="profile.userImage" v-show="!(initialProfile)" /> -->
                    <!-- Show this if the user hasn't created a profile -->
                </div>
            <div class="form-input">
                <span class="label">Name:</span>
                <input type="text" v-model="profile.name" placeholder="Enter Your Name">
            </div>
            <div class="form-input">
                <span class="label">Birthday:</span>
                <input type="datetime-local" v-model="profile.birthDate" name="birthDate"
                value="2017-06-13T13:00">
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
                    <input type="text" v-model="profile.feet" placeholder="" id="feet"> ft
                    <input type="text" v-model="profile.inches" placeholder="" id="inches"> in
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
import auth from '@/shared/auth.js' // import whether user is logged in
//import Header from 'C:/Users/Tia Smith/Pairs/c-final-capstone-tech-fitness-pal/frontend/src/components/layout/Header.vue'
import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";

// Null if user is not logged in
let user = auth.getUser();

    let initialState = {
        profile: {
            name: '',
            birthDate: '1910-01-01T00:00:00',
            currentWeight: '',
            goalWeight: '',
            feet: '',
            inches: '',
            gender: '',
            activityLevel: [],
            timeline: '',
            eatenToday: [],
            water: 0,
            userImage: "https://res.cloudinary.com/gitfit/image/upload/v1555524869/true_gpo4zq.png"
        }
    }

    Vue.use(persistentState, initialState)
     // InitialState is injected as data in all vue instances
     // Any changes to state will be stored in localStorage

export default {
    name: "profile",
    components: {
        vueDropzone: vue2Dropzone
    },
    data() {
            return {
               isAuthenticated: auth.getUser() !== null,
               dropzoneOptions: {
                // https://danhough.com/blog/dropzone-cloudinary/
                // https://alligator.io/vuejs/vue-dropzone/
                // Destination
                url: "https://api.cloudinary.com/v1_1/gitfit/image/upload",
                thumbnailWidth: 250,
                maxFilesize: 2.0,
                acceptedFiles: ".jpg, .jpeg, .png, .gif",
                uploadMultiple: false
                }
            }
    },
    created() {
        let userInCreated = auth.getUser();
            if(userInCreated !== null) {
            // Call the API to get the user's profile
            //https://localhost:44392/api/profile
            fetch(`${process.env.VUE_APP_REMOTE_API}/profile`, {
            method: 'GET',
            headers: {
                    "Authorization": 'Bearer ' + auth.getToken() 
                    }, 
            })
            .then((response) => {
            return response.json();
            }).then ((json) => {
            console.log(JSON.stringify(json));      
            this.profile.name = json.name;
            this.profile.goalWeight = json.goalWeight;
            this.profile.currentWeight = json.currentWeight;
            this.profile.birthDate = json.birthDate;
            this.profile.feet = json.feet;
            this.profile.inches = json.inches;
            this.profile.activityLevel = json.activityLevel;
            this.profile.timeline = json.timeline;
            this.profile.gender = json.gender;
            this.profile.userImage = json.userImage; 
            });
            console.log(this.profile);

            // Data is pulled back from database
            // If name is null, hide Tracking and Search in Header.vue
            // If name is null, then save isSomething to return data

        }
        console.log(this.profile);
    },
    methods: {
        ChangeUserPassword() {
            // Show form to change user password
            return true;
        },
        saveProfile() {

            let user = auth.getUser();

            if(user == null) {
                // User is not logged in so don't send the persistent state data
            }
            else {
                // User is logged in so send the profile data to the database
                fetch(process.env.VUE_APP_REMOTE_API + '/profile', {
                        method: 'POST',
                        headers: {
                        'Content-Type': 'application/json',
                        Authorization: "Bearer " + auth.getToken()
                        },
                        body: JSON.stringify(this.profile)
                        })
                        .then((response) => {
                        if(response.ok) {
                        this.$router.push('/tracking');
                        }
                        });               
            }
            // Redirect the user to the tracking page if user is not logged in
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

        afterComplete() {
            this.hasImage = true;
        },
        sending: function(file, xhr, formData) {
        formData.append("api_key", 547454926622156);
        formData.append("timestamp", (Date.now() / 1000) | 0);
        formData.append("upload_preset", "xrkbu7hz");
        },
        success: function(file, response) {
        this.profile.userImage = response.secure_url;
        },
    },
    // Check that all of the profile form fields are filled out
    computed: {
    isValidForm() {
      return this.profile.currentWeight != '' && this.profile.goalWeight != ''
       && this.profile.feet != '' && this.profile.inches != '' 
       && (this.profile.activityLevel === '1.2' || this.profile.activityLevel === '1.375' || this.profile.activityLevel === '1.55' || this.profile.activityLevel === '1.9')
       && this.profile.gender != '' && this.profile.timeline != '' && this.profile.name != '';
    },
    initialProfile() {
        return this.profile.userImage === null;
    }
  },
}

</script>




<style>

/* @media screen and (min-width: 768px) {
  img.profile-photo {
    height: 145px;
  }
}

@media screen and (min-width: 992px) {
  img.profile-photo {
    height: 160px;
  }
} */
.profile-photo {
  border-radius: 50%;
}

.img-container {
   margin-right: 25px;
   margin-top: 25px;
}


.profile-photo.small {
  height: 300px;
  width: 300px;
}

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
    min-width: 300px;
}

#gender {
    height: 35px;
}

.goal-timeline {
    height: 35px;
    width: 215px;
    /* font-size: 17px; */
}

#side-image {
    width: 570px;
    height: 570px;
    border: solid 2px red;
    margin-right: 30px;
    margin-top: 30px;
}

#profile-section {
    border: 1px black solid;
    padding: 20px;
    background-color: whitesmoke;
}

#profile-section h1 {
    /* color: #7c7474 */
}

</style>