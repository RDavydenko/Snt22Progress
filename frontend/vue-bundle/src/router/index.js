import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../pages/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/post/:id',
    name: 'PostItem',
    component: function () {
      return import('../pages/PostItem.vue')
    }
  },
  {
    path: '/voting',
    name: 'Voting',
    component: function () {
      return import('../pages/Voting.vue')
    }
  },
  {
    path: '/send-mail',
    name: 'SendMail',
    component: function () {
      return import('../pages/SendMail.vue')
    }
  },
  {
    path: '/registration',
    name: 'Registration',
    component: function () {
      return import('../pages/Registration.vue')
    }
  },
  {
    path: '/login',
    name: 'Login',
    component: function () {
      return import('../pages/Login.vue')
    }
  },
  {
    path: '/cabinet',
    name: 'Cabinet',
    component: function () {
      return import('../pages/Cabinet.vue')
    }
  },
  {
    path: '/reset-password',
    name: 'ResetPassword',
    component: function () {
      return import('../pages/ResetPassword.vue')
    }
  },
  {
    path: '/docs',
    name: 'Documents',
    component: function () {
      return import('../pages/Documents.vue')
    }
  },
  {
    path: '/government',
    name: 'Government',
    component: function () {
      return import('../pages/Government.vue')
    }
  },
  {
    path: '/plan-scheme',
    name: 'PlanScheme',
    component: function () {
      return import('../pages/PlanScheme.vue')
    }
  },
  {
    path: '/legislation',
    name: 'Legislation',
    component: function () {
      return import('../pages/Legislation.vue')
    }
  },
  {
    path: '/debtors',
    name: 'Debtors',
    component: function () {
      return import('../pages/Debtors.vue')
    }
  },
  {
    path: '/sales',
    name: 'Sales',
    component: function () {
      return import('../pages/Sales.vue')
    }
  },
  {
    path: '/sales/:id',
    name: 'SaleItem',
    component: function () {
      return import('../pages/SaleItem.vue')
    }
  },
  {
    path: '/contacts',
    name: 'Contacts',
    component: function () {
      return import('../pages/Contacts.vue')
    }
  } 
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
