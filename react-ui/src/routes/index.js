import Home from '~/pages/home'
import Product from '~/pages/product'
import Contact from '~/pages/contact'

const publicRoutes = [
    { path: '/', component: Home },
    { path: '/product', component: Product },
    { path: '/contact', component: Contact, layout: null },
]

const priveRoutes = []

export { publicRoutes, priveRoutes }