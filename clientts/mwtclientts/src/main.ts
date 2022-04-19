import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import Axios from 'axios'

Vue.config.productionTip = false

Vue.prototype.$http  =  Axios;
const  accessToken  =  localStorage.getItem('Token')

if (accessToken) {
    Vue.prototype.$http.defaults.headers.common['Authorization'] =  accessToken
}
new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
