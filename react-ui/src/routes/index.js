import Home from "~/pages/home";
import Product from "~/pages/product";
import Contact from "~/pages/contact";
import ImageUpload from "~/components/imageUpload";
import config from "~/config";

const publicRoutes = [
  { path: config.routes.home, name: "Home", component: Home },
  { path: config.routes.product, name: "Product", component: Product },
  { path: config.routes.uploadImage, name: "UploadImage", component: ImageUpload, layout: null },
  { path: config.routes.contact, name: "Contact", component: Contact, layout: null },
];

const priveRoutes = [];

export { publicRoutes, priveRoutes };
