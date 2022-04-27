import Signin from "@/components/Signin.vue";
import Dashboard from "@/views/HomeView.vue";
import Login from "@/components/Login.vue";
import CompanyDetails from "@/components/companyRegister.vue";

const routes = [
  {
    path: "/",
    name: "Signin",
    component: Signin,
  },
  {
    path: "/Dashboard",
    name: "Dashboard",
    component: Dashboard,
  },
  {
    path: "/Signin",
    name: "Signin",
    component: Signin,
  },
  {
    path: "/Signup",
    name: "Signup",
    component: () => import("../components/Signup.vue"),
  },
  {
    path: "/profile",
    name: "profile",

    component: () => import("../components/profile.vue"),
  },
  {
    path: "/companyDashboard",
    name: "companyDashboard",
    component: () => import("@/components/c_dashboard.vue"),
    children: [
      {
        path: "/companyDetails",
        name: "companyDetails",

        component: () => import("../components/companyRegister.vue"),
      },
      {
        path: "/CompanyProfile",
        name: "CompanyProfile",

        component: () => import("../components/c_profile.vue"),
      },
      {
        path: "/CompanyChangePassword",
        name: "CompanyChangePassword",

        component: () => import("@/components/c_changepassword.vue"),
      },
      {
        path: "/addproduct",
        name: "AddProduct",

        component: () => import("@/components/Company/product.vue"),
      },
      
    ],
  },
  {
    path: "/companySignup",
    name: "companySignup",

    component: () => import("@/components/CompanySignup.vue"),
  },

];

/**
 * Asynchronously load view (Webpack Lazy loading compatible)
 * The specified component must be inside the Views folder
 * @param  {string} name  the filename (basename) of the view to load.
function view(name) {
   var res= require('../components/Dashboard/Views/' + name + '.vue');
   return res;
};**/

export default routes;
