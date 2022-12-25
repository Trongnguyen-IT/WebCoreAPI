import Home from "~/pages/home";
import Product from "~/pages/product";
import Contact from "~/pages/contact";
import PhotoManager from "~/components/photoManager";
import ProductManager from "~/admin/productManager";
import config from "~/config";
import Dashboard from "~/admin/dashboard";

const publicRoutes = [
  { path: config.routes.home, name: "Home", component: Home },
  { path: config.routes.product, name: "Product", component: Product },
  {
    path: config.routes.uploadImage,
    name: "UploadImage",
    component: PhotoManager,
    layout: null,
  },
  {
    path: config.routes.contact,
    name: "Contact",
    component: Contact,
    layout: null,
  },
  {
    path: config.routes.dashboard,
    name: "Dashboard",
    component: Dashboard,
    layout: "admin",
  },
  {
    path: config.routes.productManager,
    name: "Product Manager",
    component: ProductManager,
    layout: "admin",
  },
];

const priveRoutes = [];

export { publicRoutes, priveRoutes };
