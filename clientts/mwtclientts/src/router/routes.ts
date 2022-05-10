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
    children: [   
      {
        path: "/userproductlist",
        name: "userproductlist",
        component: () => import("@/components/userproduct_list.vue"),
      },
      {
        path: "/productDetails/:id?",
        name: "productDetails",
        component: () => import("@/components/product_details.vue"),
      },
      {
        path: "/profile",
        name: "profile",
    
        component: () => import("../components/profile.vue"),
      },
      {
        path: "/changepassword",
        name: "changepassword",
    
        component: () => import("@/components/change_password.vue"),
      },
      {
        path: "/checkout",
        name: "checkout",
    
        component: () => import("@/components/checkout.vue"),
      },
      {
        path: "/payment",
        name: "payment",
    
        component: () => import("@/components/payment.vue"),
      },
    ],
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
    path: "/AddAddress",
    name: "AddAddress",

    component: () => import("../components/user_address.vue"),
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
        path: "/UpdatecompanyDetails",
        name: "UpdatecompanyDetails",

        component: () =>
          import("@/components/Company/Update_comapnydetails.vue"),
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
      {
        path: "/productlist",
        name: "ProductList",

        component: () => import("@/components/Company/product_list.vue"),
      },
      {
        path: "/updateproduct",
        name: "updateproduct",

        component: () => import("@/components/Company/update_product.vue"),
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
