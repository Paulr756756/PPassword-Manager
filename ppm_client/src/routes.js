import { createRouter, createWebHashHistory } from "vue-router"
import Hero from "./components/Hero.vue"

const routes = [
    { path: '/', component: Hero}
]

const router = createRouter({
    history: createWebHashHistory(),
    routes,
})

export default router