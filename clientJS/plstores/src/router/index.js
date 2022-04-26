import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Login from '@/components/Login'
import Layout from '@/components/Layout'
import updateuserprofile from '@/components/updateuserprofile'
import companyregister from '@/components/companyregister'
import newupdate from '@/components/newupdate'
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      component: Layout,
      children:[
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
        path:'/newupdate',
        component:newupdate,
        name:'newupdate',
      },

        
      ]

    }
  ],
    mode:'history'
},

  )
