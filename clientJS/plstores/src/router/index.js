import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Login from '@/components/Login'
import Layout from '@/components/Layout'
import updateuserprofile from '@/components/updateuserprofile'
import companyregister from '@/components/companyregister'
import resetpassword from'@/components/resetpassword'
import companydashboard from'@/components/companydashboard'
import addproduct from '@/components/addproduct'
import displayproducts from'@/components/displayproducts'
import editproducts from'@/components/editproducts'
import updateaddress from'@/components/updateaddress'
import companydetails from'@/components/companydetails'
Vue.use(Router)

export default new Router({
  routes: [
  
        {
          path:'/',
          component:Home,
          name:'Home'
        },
       {
         path:'/Login',
         component:Login,
         name:'Login',
       },
       {
        path:'/updateuserprofile',
        component:updateuserprofile,
        name:'updateuserprofile',
      },
      {
        path:'/companyregister',
        component:companyregister,
        name:'companyregister',
      },
      {
        path:'/resetpassword',
        component:resetpassword,
        name:'resetpassword',
      },
      {
        path:'/companydashboard',
        component:companydashboard,
        name:'companydashboard',
        children:[
          {
            path:'/addproduct',
            component:addproduct,
            name:'addproduct',
            
    
          },
          {
            path:'/displayproducts',
            component:displayproducts,
            name:'displayproducts',
          },
        ],
      },
     
      
      
      {
        path:'/editproducts',
        component:editproducts,
        name:'editproducts',
      },
      {
        path:'/updateaddress',
        component:updateaddress,
        name:'updateaddress',
      },
      {
        path:'/companydetails',
        component:companydetails,
        name:'companydetails',
      },
        
      ]

    },
  
  {
    mode:'history'
    
  }
   


  )
