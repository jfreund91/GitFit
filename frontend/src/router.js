import Vue from "vue";
import Router from "vue-router";
import Login from "./views/Login.vue";
import Profile from "./views/Profile.vue";
import Tracking from "./views/Tracking.vue";
import Search from "./views/Search.vue";
import Landing from "./views/Landing.vue";
import Modal from "./views/Modal.vue";
import auth from "./shared/auth";

Vue.use(Router);

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authentiated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiesAuth: false
      }
    },
    {
      path: "/profile",
      name: "profile",
      component: Profile,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/tracking",
      name: "tracking",
      component: Tracking,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/search",
      name: "search",
      component: Search,
      meta: {
        requiesAuth: true
      }
    },
    {
      path: "/",
      name: "landing",
      component: Landing,
      meta: {
        requiesAuth: true
      }
    },
    {
      path: "/modal",
      name: "modal",
      component: Modal,
      meta: {
        requiesAuth: true
      }
    }
  ]
});

router.beforeEach((to, from, next) => {
// Determine if the route requires Authentication
const requiresAuth = to.matched.some(x => x.meta.requiresAuth);
const user = auth.getUser();

// If it does and they are not logged in, send the user to "/login"
if (requiresAuth && !user) {
  next("/login");
  } else {
// Else let them go to their next destination
    next();
    }
});

export default router;
