import { createRouter, createWebHashHistory } from "vue-router"
import Home from "./components/Home.vue"
import Register from "./components/Register.vue"
import Login from "./components/Login.vue"
import UserHome from "./components/UserHome.vue"
import useAuth from "./auth"

const routes = [
    { path: '/', component: Home },
    {
        path: '/register',
        name: 'register',
        component: Register,
        meta: {requiresAuth: false}
    },
    {
        path: '/login',
        name: 'login',
        component: Login,
        meta: {requiresAuth: false}
    },
    {
        path: '/user',
        name: 'userhome',
        component: UserHome,
        meta: {requiresAuth : true}
    }
/*     {
        path: '/users/:username',
        component: User,
        children: [
          // UserHome will be rendered inside User's <router-view>
          // when /users/:username is matched
          { path: '', component: UserHome },
  
          // UserProfile will be rendered inside User's <router-view>
          // when /users/:username/profile is matched
          { path: 'profile', component: UserProfile },
  
          // UserPosts will be rendered inside User's <router-view>
          // when /users/:username/posts is matched
          { path: 'posts', component: UserPosts },
        ],
      }, */
]

const router = createRouter({
    history: createWebHashHistory(),
    routes,
    mode: 'history'
})

 /*
      Before each route, check if
      route requires authentication, if true then
      check if isAuthenticated = true, else
      check if accessToken is not null, if not then redirect to login
      if true, then check if token has not expired,if not true then
      use refreshtoken to get new accesstokens
*/
router.beforeEach(async (to, from) => {
    if (to.meta.requiresAuth && !isAuthenticated.value) {
        const expireDate = parseInt(localStorage.getItem('expireDate'))
        if (localStorage.getItem('accessToken') === null) {
            return { name: 'login' }
        } 
        else if (Date.now() <= expireDate) {
            isAuthenticated.value = true
        } else if (Date.now() > expireDate) {
            await refreshLogin()
            isAuthenticated.value = true
        }
    } 
})

export default router
const { isAuthenticated, refreshLogin } = useAuth()
