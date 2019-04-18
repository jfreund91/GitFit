<template>
    <div id="login">
        <section id="login-signup" v-bind:class="{ showSignupForm: !showLoginForm }">
            <form v-if="showLoginForm" v-on:submit.prevent="login">
            <p v-bind:error="error"></p>
            <h1>Login</h1>

            <label for="email">Username</label>
            <input
              v-model.trim="loginForm.username"
              type="text"
              placeholder="Username"
              id="email"
            />

            <label for="password">Password</label>
            <input
              v-model.trim="loginForm.password"
              type="password"
              placeholder="Password"
              id="password"
            />

        <div class="form-actions">
          <button class="login">Log In</button>
          <div class="extras">
            <button v-on:click="toggleForm">Create Account</button>
            </div>
        </div>
      </form>

      <form v-else v-on:submit.prevent="signup">
        <h1>Git Started</h1>

        <p :error="error"></p>

        <label for="email2">Username</label>
        <input
          v-model.trim="signupForm.username"
          type="text"
          placeholder="Username"
          id="email2"
        />

        <label for="password2">Password</label>
        <input
          v-model.trim="signupForm.password"
          type="password"
          placeholder="Min 6 characters"
          id="password2"
        />

        <label for="password3">Confirm Password</label>
        <input
          v-model.trim="signupForm.confirmPassword"
          type="password"
          placeholder="Confirm password"
          id="password3"
        />

        <div class="form-actions">
          <button class="login">Sign Up</button>
          <div class="extras">
            <button v-on:click="toggleForm">Already have an Account?</button>
          </div>
        </div>
      </form>
    </section>
  </div>            
</template>

<script>
import auth from "@/shared/auth";
import prof from "@/shared/prof"

export default {
  data() {
    return {
      showLoginForm: true,
      error: "",
      /** Contents of the login form */
      loginForm: {
        username: "",
        password: ""
      },
      /** Contents of the sign up form */
      signupForm: {
        password: "",
        username: "",
        role: "user"
      }
    };
  },
  methods: {
    /**
     * Toggles the showLoginform
     * @function
     */
    toggleForm() {
      this.showLoginForm = !this.showLoginForm;
      this.error = "";
    },
    /**
     * Navigates the user to the home route.
     * @function
     */
    goHome() {
      this.$router.push("/");
    },
    goToProfile() {
      this.$router.push("/profile");
    },
    /**
     * Logs the user in and then sends them to the dashboard.
     * NOTE: Uses async/await
     */
    async login() {
      this.error = "";

      /**
       * This example uses async/await over Promise .then()
       * Under the hood it still relies on promises but the syntax of
       * await Promise makes it a bit shorter and easier to read.
       * It also helps with one issue which is the need to conditionally
       * run logic based on the response.statusCode.
       */
      try {
        const url = `${process.env.VUE_APP_REMOTE_API}/account/login`;
        const response = await fetch(url, {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json"
          },
          body: JSON.stringify(this.loginForm)
        });

        if (response.status === 401) {
          this.error = "Your username and/or password is invalid"; // print this to user
          this.loginForm.password = "";
        } else {
          // Parse output and save authentication token
          const token = await response.json(); // response json body
          auth.saveToken(token);
          //this.goHome(); // Run method to send user to homepage
          // Make a call to get the profile stats from the database

          // Send the user to the profile page with values populated from the database if any
          this.goToProfile();
        }
      } catch (error) {
        console.error(error);
        this.error = "There was an error logging in";
      }
      //prof.destroyProfile(); // Remove profile from local storage upon login so Header logic works
    },
    /**
     * Signs the user up and then redirects them to the dashboard.
     */
    async signup() {
      this.error = "";

      try {
        const url = `${process.env.VUE_APP_REMOTE_API}/account/register`;
        const response = await fetch(url, {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json"
          },
          body: JSON.stringify(this.signupForm)
        });

        // We definitely need the response if success or not.
        const data = await response.json();

        if (response.status === 400) {
          this.error = data.message;
        } else {
          auth.saveToken(data);
          const user = auth.getUser();
          this.goToProfile();
        }
      } catch (error) {
        console.error(error);
        this.error = "There was an error attempting to register";
      }
    }
  }
};
</script>
<style scoped>

#login {
  display: flex;
  height: 100vh;
  flex-direction: column;
}

.extras, .login {
  width: 30%;
}

#content {
  display: initial;
  background-color: #00adee;
  color: #fff;
}

#content,
#login-signup {
  padding: 5vh 1rem 1rem 1rem;
}

#content h1,
#login-signup h1 {
  margin-bottom: 2rem;
}

form h1 {
  margin-left: 0;
}

form label {
  font-size: 1rem;
  margin-bottom: 0.5rem;
}

form input {
  width: 100%;
  margin-bottom: 1rem;

}

form label,
form input {
  display: block;
}


.form-actions {
  display: flex;
  justify-content: space-between;
}


.form-actions .extras {
  text-align: right;
  align-self: center;
}

@media screen and (min-width: 768px) {
  #login-signup {
    padding-top: 10vh;
  }

  #content > *,
  #login-signup form {
    max-width: 40%;
    margin: 0 auto;
  }
}

@media screen and (min-width: 1024px) {
  #content h1,
  #login-signup h1 {
    margin-bottom: 2rem;
  }

  #login {
    flex-direction: row;
  }

  #content,
  #login-signup {
    flex: 1;
    padding: 1rem 1rem 1rem 1rem;
  }

  #login-signup.showSignupForm {
    padding-top: 1rem;
  }
}

#register-header {
  text-align: center;
}

</style>



