<template>
<div class="change-password">
    <h1>Change Password</h1>
    <form v-on:submit.prevent="changepword">
        <div class="form-input">
            <label for="password">Current Password:</label>
            <!-- <span class="label">New Password:</span> -->
            <input type="password" placeholder="Enter Current Password"
            id="password"
            v-model.trim="changePasswordForm.password">
        </div>
        <div class="form-input">
            <label for="newpassword">New Password:</label>
            <!-- <span class="label">New Password:</span> -->
            <input type="password" placeholder="Enter New Password"
            id="newpassword"
            v-model.trim="changePasswordForm.newpassword">
        </div>
        <div class="form-input">
            <label for="newpassword_repeat">Confirm New Password:</label>
            <!-- <span class="label">Confirm New Password:</span> -->
            <input type="password" placeholder="Confirm New Password"
            id="newpassword_repeat"
            v-model.trim="changePasswordForm.password_repeat">
        </div>
        <button class="change-password-btn" type="submit">Change Password</button>
    </form>


</div>
</template>

<script>
import auth from "@/shared/auth";
import prof from "@/shared/prof"

let user = auth.getUser();

export default {
    data() {
        return {
            /** Contents of the change password form */
            changePasswordForm: {
                newpassword: "",
                newpassword_repeat: "",
                username: user.sub, // pull from authentication credentials - user.sub
                password: "", // have user enter old password
            }
        }
    },
    methods: {
        goBackToProfile() {
            this.$router.push("/profile");
        },
        // Change the password on the database
        async changepword() {
            try {
                const url = `${process.env.VUE_APP_REMOTE_API}/account/changePassword`;
                const response = await fetch(url, {
                method: "POST",
                headers: {
                    Accept: "application/json",
                    "Content-Type": "application/json"
                },
                // Sending the new passwords
                body: JSON.stringify(this.changePasswordForm)
                });

                // We definitely need the response if success or not.
                const data = await response.json();

                if (response.status === 400) {
                    this.error = data.message;
                } 
                else {
                    auth.saveToken(data);
                    const user = auth.getUser();
                    this.goBackToProfile();
                }
            } catch (error) {
                console.error(error);
                this.error = "There was an error attempting to register";
            }
        }
    }
}

</script>

<style scoped>
.change-password {
    display: flex;
    height: 100vh;
    flex-direction: column;
    padding-left: 35%;
    /* margin: 50px auto;
    text-align: center;
    padding-top: 0px;
    display: flex;
    margin-left: 10px;
    justify-content: center;
    margin-right: 10px;
    flex-direction: column; */
}

.form-input{
    margin: 15px 0px;
    display: flex;
    /* justify-content: flex-start; */
    justify-content: start;
}

.change-password-btn {
    width: 400px;
}

</style>
