import { createRouter, createWebHistory } from 'vue-router'
import Dashboard from '../views/Dashboard.vue'
import Setup from '../views/Setup.vue'
import AddContainerDefinition from '../views/AddContainerDefinition.vue'

const routes = [
  {
    path: '/',
    name: 'Test',
    component: Dashboard
  },
  {
    path: '/setup',
    name: 'Setup',
    component: Setup
  },
  {
    path: '/add/container-def',
    name: 'Container definition',
    component: AddContainerDefinition
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
