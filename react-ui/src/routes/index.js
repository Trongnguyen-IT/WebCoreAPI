import Home from "~/pages/home";
import Product from "~/pages/product";
import Contact from "~/pages/contact";
import ImageLoad from "~/components/imageUpload/imageUpload";

const publicRoutes = [
  { path: "/", name: "Home", component: Home },
  { path: "/product", name: "Product", component: Product },
  { path: "/upload-image", name: "UploadImage", component: ImageLoad },
  { path: "/contact", name: "Contact", component: Contact, layout: null },
];

const priveRoutes = [];

export { publicRoutes, priveRoutes };
