import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Caixa/Create.vue'
import CaixaCriar from '../views/Caixa/Create.vue'
import CaixaListar from '../views/Caixa/Index.vue'
import EstoqueCriar from '../views/Estoque/Create.vue'
import EstoqueListar from '../views/Estoque/Index.vue'
import EstoqueEditar from '../views/Estoque/Edit.vue'
const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/Caixa/Criar',
    name: 'CaixaCriar',
    component: CaixaCriar
  },
  {
    path: '/Caixa/Listar',
    name: 'CaixaListar',
    component: CaixaListar
  },
  {
    path: '/Estoque/Criar',
    name: 'EstoqueCriar',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: EstoqueCriar
  },
  {
    path: '/Estoque/Listar',
    name: 'EstoqueListar',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: EstoqueListar
  },
  {
    path: '/Estoque/Editar',
    name: 'EstoqueEditar',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: EstoqueEditar
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
