export default {
    /**
     * Returns the profile from local storage.
     * @function
     */
    getProfile() {
      return localStorage.getItem("profile");
    },
    /**
     * Remove profile from local storage
     */
    destroyProfile() {
      localStorage.removeItem("profile");
    },
  
    /**
     * Checks that user's profile is already filled out.
     * @returns false if profile is not filled out.
     * @function
     */
    isValidProfile() { //auth.getUser();
      let profile = JSON.parse(localStorage.getItem("profile"));
      let isValid = false;

        // Check that all fields in profile are filled out
        if(profile.currentWeight !== ''  && profile.goalWeight !== '' && profile.feet !== '' && profile.inches !== ''
        && profile.gender !== '' && profile.timeline !== '' && profile.name !== '' && profile.birthDate > '1909-01-01T00:00:00'){
            isValid = true;
        }

        return isValid;

    },

    

  };